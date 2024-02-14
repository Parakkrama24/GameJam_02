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
    public  float  health = 100;
    [SerializeField] private GameObject PlayerBulate;
    [SerializeField] float bulletSpeed = 100f;


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

        if(Input.GetMouseButtonDown(0))
        {
            Player_shooting();
        }
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
        Debug.Log("Shoot");

        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position to world coordinates
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Ensure the Z coordinate is 0 since this is a 2D game
        worldPosition.z = 0f;

        // Instantiate the bullet at the player's position
        GameObject bullet = Instantiate(PlayerBulate, transform.position, Quaternion.identity);

        // Calculate the direction towards the mouse click position
        Vector3 direction = (worldPosition - transform.position).normalized;

        // Set the bullet's velocity to move towards the mouse click position
 // Adjust the speed as needed
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        Destroy(bullet, 2f);
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
