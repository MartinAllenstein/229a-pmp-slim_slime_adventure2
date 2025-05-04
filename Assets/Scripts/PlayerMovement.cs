using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animator;

    Vector2 moveInput;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private bool isJumping;
    
    private bool facingRight = true;
    
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Walk
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal") , 0);
        animator.SetFloat("Speed", Mathf.Abs(moveInput.x));
        
        //Jump
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
            //Debug.Log("Jump!");
            
            animator.SetBool("IsJumping", true);
        }
        
        //flip player
        if (moveInput.x > 0 && !facingRight)
        {
            Flip();
        }
        
        else if (moveInput.x < 0 && facingRight)
        {
            Flip();
        }
    }
    

    void FixedUpdate()
    {
        //rb2d.AddForce(moveInput * speed);
        rb2d.linearVelocity = new Vector2(moveInput.x * speed, rb2d.linearVelocity.y);
        animator.SetFloat("yVelocity", rb2d.linearVelocity.y);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
