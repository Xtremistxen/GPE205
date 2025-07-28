using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Player Health: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage! Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth); // Clamp to max
        Debug.Log("Player healed! Current health: " + currentHealth);
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Spike"))
    {
        Die();
    }
}

    void Die()
    {
        Debug.Log("Player died!");
        // Add respawn logic, game over screen, etc.
        gameObject.SetActive(false);
    }
}

