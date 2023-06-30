using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]  private float speed = 3f;

    private enum State
    {
        Idle,
        Normal,
        Rolling,
    }
    private State state;
    private Rigidbody2D playerRb;
    public Vector2 moveInput {get; private set;}
    public Vector2 lastMove {get; private set;}
    private Animator playerAnimator;
    public Vector2 rollDirection {get; private set;}
    private float lastX = 0f, lastY = 0f;

    void Awake()
    {
        playerRb = this.GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        state = State.Idle;
    }
    void Start()
    {
        //playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX, moveY;
        
        switch(state)
        {
            case State.Normal:
                moveX = Input.GetAxisRaw("Horizontal");
                moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;
        
        playerAnimator.SetFloat("Horizontal", moveX);
        playerAnimator.SetFloat("Vertical", moveY);
        playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude);

                if(moveX == 0f && moveY == 0f)
                {
                    state = State.Idle;
                    break;
                }

                lastX = moveX;
                lastY = moveY;
                lastMove = new Vector2(lastX, lastY).normalized;
                break;
            case State.Idle:
                if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
                {
                    state = State.Normal;
                }
                break;
            case State.Rolling:
                //! Por Implementar
                break;
        }

    }

    void FixedUpdate()
    {
        switch(state)
        {
            case State.Normal:
                playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);
                break;
            case State.Rolling:
                //! Por Implementar
                Dash();
                break;
    }
        playerAnimator.SetFloat("Horizontal", lastX);
        playerAnimator.SetFloat("Vertical", lastY);
        
    }

    //! Por Implementar
    void Dash()
    {
        rollDirection = moveInput;
        playerRb.velocity = rollDirection;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        playerRb.inertia = 0f;   
        playerRb.velocity = Vector2.zero;
        playerRb.angularVelocity = 0f;
    }

}
