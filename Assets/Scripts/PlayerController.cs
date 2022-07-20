using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private enum State
    {
        Idle,
        Run,
        Jump,
        Throw
    }

    private State currentState = State.Idle;
    private Animator _animator;
    [SerializeField] private float runSpeed = 10f;

    private float? lastGroundedTime;
    private bool isJumping;
    private bool isGrounded;

    [SerializeField]
    private float distToGround;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

// Update is called once per frame
    void Update()
    {
        currentState = State.Idle;
        MovementControl();
    }
    private bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1);
    }
    private void MovementControl()
    {
        if (isGrounded)
        {
            lastGroundedTime = Time.time;
        }
        if (currentState == State.Idle)
        {
            // _animator.SetBool("isMoving", false);
        }
        else if (currentState == State.Run)
        {
            Run();
        }
        else if (currentState == State.Jump)
        {
            Jump();
        }
    }

    private void Run()
    {
        _animator.SetBool("isMoving", true);
        this.transform.Translate(Vector3.forward * Time.deltaTime);
    }

    public void Jump()
    {
        _animator.SetBool("isJumping", true);
        Debug.Log("Jumping");
    }
}