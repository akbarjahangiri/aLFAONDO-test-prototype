using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : Singleton<InputManager>
{
    #region Events

    public delegate void StartTouch(Vector2 position, float time);

    public event StartTouch OnStartTouch;

    public delegate void EndTouch(Vector2 position, float time);

    public event EndTouch OnEndTouch;

    #endregion

    private TouchControls _touchControls;
    private Camera _mainCamera;

    public override void Awake()
    {
        _touchControls = new TouchControls();
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _touchControls.Enable();
    }

    private void OnDisable()
    {
        _touchControls.Disable();
    }

    void Start()
    {
        _touchControls.Touch.PrimaryContact.started += context => StartTouchPrimary(context);
        _touchControls.Touch.PrimaryContact.canceled += context => EndTouchPrimary(context);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null)
        {
            OnStartTouch(Utils.ScreenToWorld(_mainCamera, _touchControls.Touch.PrimaryPosition.ReadValue<Vector2>()),
                (float) context.startTime);
        }
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
        {
            OnEndTouch(Utils.ScreenToWorld(_mainCamera, _touchControls.Touch.PrimaryPosition.ReadValue<Vector2>()),
                (float) context.time);
        }
    }

    public Vector2 FingerPrimaryPosition()
    {
        return Utils.ScreenToWorld(_mainCamera, _touchControls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}