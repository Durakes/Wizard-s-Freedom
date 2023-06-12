using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]  private float speed = 3f;
    private Rigidbody2D playerRb;
    public Vector2 moveInput {get; private set;}
    private Animator playerAnimator;
    void Awake()
    {
        playerRb = this.GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }
    void Start()
    {
        //playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;
        
        playerAnimator.SetFloat("Horizontal", moveX);
        playerAnimator.SetFloat("Vertical", moveY);
        playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude);

    }

    void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);
    }

}
