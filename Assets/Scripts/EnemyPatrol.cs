using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;

    public Transform groundCheck;   // Empty GameObject at feet front
    public float groundCheckDistance = 0.2f;

    public Transform wallCheck;     // Empty GameObject at head/front
    public float wallCheckDistance = 0.2f;

    public LayerMask groundLayer;   // Layer for ground/walls

    public Transform player;            // Assign the player in Inspector
    public float detectionRange = 5f;   // Enemy starts chasing in this range
    public float attackRange = 1f;      // Enemy attacks when this close

    private Rigidbody2D rb;
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            rb.velocity = Vector2.zero;
            AttackPlayer();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        rb.velocity = new Vector2(speed * (movingRight ? 1 : -1), rb.velocity.y);

        // Check for ground ahead
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        // Check for wall ahead
        Vector2 wallDirection = movingRight ? Vector2.right : Vector2.left;
        RaycastHit2D wallInfo = Physics2D.Raycast(wallCheck.position, wallDirection, wallCheckDistance, groundLayer);

        if (!groundInfo || wallInfo)
        {
            Flip();
        }
    }

    void ChasePlayer()
    {
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        // Flip sprite to face the player
        if ((direction > 0 && !movingRight) || (direction < 0 && movingRight))
        {
            Flip();
        }
    }

    void AttackPlayer()
    {
        Debug.Log("Enemy attacks the player!");
        // Add damage or animation trigger here
    }
    void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(1);
        }
    }
}


    void Flip()
    {
        movingRight = !movingRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        }
        if (wallCheck != null)
        {
            Gizmos.color = Color.blue;
            Vector3 dir = movingRight ? Vector3.right : Vector3.left;
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + dir * wallCheckDistance);
        }

        // Optional: draw detection + attack radius
        if (player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
