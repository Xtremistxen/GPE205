using UnityEngine;

public class EnemyHead : MonoBehaviour 
{
    public EnemyHealth enemyHealth;
    public int damageAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Damage the enemy
            enemyHealth.TakeDamage(damageAmount);

            // Bounce player upward
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 10f); // Adjust bounce strength
            }
        }
    }
}

