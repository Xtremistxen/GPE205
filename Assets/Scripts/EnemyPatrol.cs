using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;

    public Transform groundCheck;   // Empty GameObject at feet front
    public float groundCheckDistance = 0.2f;

    public Transform wallCheck;     // New Empty GameObject at head/front
    public float wallCheckDistance = 0.2f;

    public LayerMask groundLayer;   // Layer for ground/walls

    private Rigidbody2D rb;
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        // Move left or right
        rb.velocity = new Vector2(speed * (movingRight ? 1 : -1), rb.velocity.y);

        // Check for ground ahead
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        // Check for wall ahead
        Vector2 wallDirection = movingRight ? Vector2.right : Vector2.left;
        RaycastHit2D wallInfo = Physics2D.Raycast(wallCheck.position, wallDirection, wallCheckDistance, groundLayer);

        if (groundInfo.collider == false || wallInfo.collider == true)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Flip sprite horizontally
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
    }

}

