using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator; // This will be a reference to our animations used
    public int speed = 1; // Horizontal speed
    public float jumpForce = 0f; // How strong the jump is

    private Rigidbody2D characterBody;
    private Vector2 inputMovement;

    public Transform groundCheck; // Empty GameObject at feet
    public float groundCheckRadius = 0f;
    public LayerMask groundLayer;

    private bool isGrounded;

    // ✅ Flip support
    private bool facingRight = true;

    void Start()
    {
        characterBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Grabs Animator
    }

    void Update()
    {
        // Horizontal input only
        inputMovement = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //jump parameter
        animator.SetBool("IsJumping", !isGrounded);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            characterBody.velocity = new Vector2(characterBody.velocity.x, jumpForce);
        }

        // ✅ Flip check
        if (inputMovement.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (inputMovement.x < 0 && facingRight)
        {
            Flip();
        }

        animator.SetFloat("Run", Mathf.Abs(inputMovement.x));

    }

    void FixedUpdate()
    {
        // Only set horizontal velocity — leave Y alone!
        characterBody.velocity = new Vector2(inputMovement.x * speed, characterBody.velocity.y);
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Flip X axis
        transform.localScale = localScale;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    internal void TakeDamage(int v)
    {
        throw new NotImplementedException();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Check if the collision was from above
            if (transform.position.y > collision.transform.position.y + 0.3f)
            {
                // Damage the enemy
                EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(1);
                }

                // Bounce the player up
                GetComponent<Rigidbody2D>().velocity = new Vector2(
                    GetComponent<Rigidbody2D>().velocity.x,
                    10f); // Bounce force
            }
        }
    }
}

