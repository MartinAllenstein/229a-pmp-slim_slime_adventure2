using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb2d;

    Vector2 moveInput;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private bool isJumping;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Walk
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        
        //Jump
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
            //rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //Debug.Log("Jump!");
        }
    }

    void FixedUpdate()
    {
        //rb2d.AddForce(moveInput * speed);
        rb2d.linearVelocity = new Vector2(moveInput.x * speed, rb2d.linearVelocity.y);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }
}
