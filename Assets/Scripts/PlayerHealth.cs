using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;// Players starting health
    public int currentHealth;// Cheacks players current health

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)//Damage from a source takes away health
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()// Pleayers death when reaches 0 life
    {
        Debug.Log("Player died!");
        Destroy(gameObject);
    }
}
