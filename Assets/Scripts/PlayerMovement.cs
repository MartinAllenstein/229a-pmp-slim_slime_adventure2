using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animator;

    Vector2 moveInput;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private bool isJumping;
    
    private bool facingRight = true;

    private int goldCoin = 0 ;
    [SerializeField] private Text goldCoinText ;
    [SerializeField] private Text goldCoinFinalText;
    private float time = 0f;
    [SerializeField] private Text timeText;
    [SerializeField] private Text timeFinalText;

    [SerializeField] private bool isGameActive = true;
    [SerializeField] private bool isWin = false;

    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public GameObject gameWinScreen;

    public Button playButton;
    public Button exitButton;
    public Button replayButton;
    public Button replayButton2;

    public AudioClip jumpSound;
    public AudioClip hitSound;
    public AudioSource audioSource;

    private void Awake()
    {
        playButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(Exit);
        replayButton.onClick.AddListener(Restart);
        replayButton2.onClick.AddListener(Restart2);
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Time.timeScale = 0f;
        gameWinScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        titleScreen.SetActive(true);
    }

    void Update()
    {
        goldCoinText.text = goldCoin.ToString();
        goldCoinFinalText.text = goldCoin.ToString();

        //Walk
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal") , 0);
        animator.SetFloat("Speed", Mathf.Abs(moveInput.x));
        
        //Jump
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
            //Debug.Log("Jump!");
            audioSource.PlayOneShot(jumpSound);
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

        if (!isGameActive)
        {
            GameOver();
        }
        if (isWin)
        {
            GameWin();
        }
    }
    

    void FixedUpdate()
    {
        time += Time.deltaTime;
        timeText.text = time.ToString("0.0");
        timeFinalText.text = time.ToString("0.0");

        rb2d.AddForce(moveInput * speed);
        //rb2d.linearVelocity = new Vector2(moveInput.x * speed * 0.1f, rb2d.linearVelocity.y);
        animator.SetFloat("yVelocity", rb2d.linearVelocity.y);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
        if (other.gameObject.CompareTag("GameOver"))
        {
            isGameActive = false;
        }
        if (other.gameObject.CompareTag("GameWin"))
        {
            isWin = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gold"))
        {
            goldCoin++;
            audioSource.PlayOneShot(hitSound);
            Destroy(collision.gameObject);
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

    void Exit()
    {
        Application.Quit();
    }

    void StartGame()
    {
        Time.timeScale = 1f;
        titleScreen.SetActive(false);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }

    public void GameWin()
    {
        Time.timeScale = 0f;
        gameWinScreen.SetActive(true);
        //Destroy(timeText);
    }

    public void Restart()
    {
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

    public void Restart2()
    {
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }
}
