using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 2;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took damage! Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died!");
        // When dead either does animation or drops loot.
        Destroy(gameObject);
    }
}

