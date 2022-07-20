using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private float minimumDistance = 0.2f;
    [SerializeField] private float maximumTime = 1f;
    [SerializeField, Range(0f, 1f)] private float directionThreshold = 0.9f;
    private InputManager _inputManager;
    private Vector2 _startPosition;
    private float _startTime;
    private Vector2 _endPosition;
    private float _endTime;
    public GameEvent ev_swipeUp;

    private void Awake()
    {
        _inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        _inputManager.OnStartTouch += SwipeStart;
        _inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        _inputManager.OnStartTouch -= SwipeStart;
        _inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        _startPosition = position;
        _startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        _endPosition = position;
        _endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector2.Distance(_startPosition, _endPosition) >= minimumDistance && (_endTime - _startTime) <= maximumTime)
        {
    
            Vector3 direction = _endPosition - _startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            if (ev_swipeUp != null)
            {
                ev_swipeUp.Raise();
            }

        }

        //else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        // {
        //     Debug.Log("Swipe down");
        // }
        //
        //else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        // {
        //     Debug.Log("Swipe right");
        // }
        //
        //else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        // {
        //     Debug.Log("Swipe left");
        // }
    }
}