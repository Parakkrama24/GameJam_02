using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    [SerializeField] private float dashTimeDuration = 0.2f;
    private float dashSpeed;
    [SerializeField] private int dashPower=5;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isFlip;
    private SpriteRenderer playerSpriteRender;
    private Animator animator;
    private bool isDash= false;
   [SerializeField] private float health = 100;
    [SerializeField] private GameObject PlayerBulate;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSpriteRender=GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();    
        playerSpriteRender.flipX = false;
        
        Debug.Log(dashSpeed);
    }

    void Update()
    {
        // Horizontal movement
        if (isGrounded)
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        


        if (moveInput!=0)
        {
            if (moveInput < 0)
            {
                isFlip = true;
                dashSpeed =- moveSpeed * dashPower;

            }
            else if (moveInput > 0)
            {
                isFlip = false;
                dashSpeed = moveSpeed * dashPower;
            }
            isDash = false;
            animator.SetBool("Iswalk",true);
        }
            
        else
        {
            isDash = true;
            animator.SetBool("Iswalk", false);
        }
        
        if(isFlip)
        {
            playerSpriteRender.flipX = true;
        }
        else
        {
            playerSpriteRender.flipX= false;
        }
        


        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetTrigger("IsJump");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && !isDash)
        {
            StartCoroutine(Dash());
            animator.SetTrigger("IsDash");
        }
        }
        else
        {
            animator.SetBool("Iswalk", false);
        }

        Player_shooting();
    }
   
  

    private IEnumerator Dash()
    {
        isDash = true;
  
        float startTime = Time.time;
        while(Time.time < startTime+dashTimeDuration)
        {
            rb.velocity= new Vector2(rb.velocity.x+dashSpeed,rb.velocity.y);
            yield return null;
        }

        isDash = false;
    }

    private void Player_shooting()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Shhot");
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            worldPosition.z = 0f;
           // Debug.Log("Mouse Click Position: " + worldPosition);


        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Bulate"))
        {
            health -= 10;
        }
    }

    
}
