using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private int health;
    private Animator anim;
    //public Camera camera;
    public Rigidbody2D rb;
    bool isFacingRight = true;
    private float walking;
    public float speed = 5;

    [Header("Jumping")]
    public float jumpPower = 20f;
    private int maxJumps = 1;
    int jumpsRemaining;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 4f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 3;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector2(transform.position.x + walking * speed * Time.deltaTime, transform.position.y);
        Camera.main.transform.position = new Vector3(transform.position.x + 5, Camera.main.transform.position.y, Camera.main.transform.position.z);
        rb.linearVelocity = new Vector2(walking * speed, rb.linearVelocityY);
        GroundCheck();
        Gravity();
        Flip();
        if(health == 0)
        {
            SceneManager.LoadSceneAsync("Game Over");
        }
    }

    void OnMove(InputValue value)
    {
        walking = value.Get<Vector2>().x;
        if (isGrounded)
        {
            bool moving = walking != 0;
            anim.SetBool("isWalking", moving);
        }
    }

    void OnJump(InputValue value)
    {
        if(jumpsRemaining > 0)
        {
            if (value.isPressed == true)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpPower);
                jumpsRemaining--;
            } 
            else if (value.isPressed == false) 
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY * 0.5f);
                jumpsRemaining--;
            }
        }
    }

    void OnLowA(InputValue value)
    {
        anim.SetBool("lowSweep", value.isPressed);
    }

    void OnHighA(InputValue value)
    {
        anim.SetBool("highSweep", value.isPressed);
    }

    private void Gravity()
    {
        if(rb.linearVelocityY < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Max(rb.linearVelocityY, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }
    private void GroundCheck()
    {
        if(Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        anim.SetBool("airTime", !isGrounded);
    }

    private void Flip()
    {
        if(isFacingRight && walking < 0 || !isFacingRight && walking > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag.Equals("Enemy")){
            Destroy(other.gameObject);
            health--;
        }
    }

}
