using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerStat playerStats;
    public int currentPlayerHealth;
    public int currentPlayerDamage;
    public HealthBar healthBar;
    public GameObject playerDeathEffect; // Reference to the death effect prefab

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPlayerHealth = playerStats.playerHealth;
        currentPlayerDamage = playerStats.playerDamage;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(playerStats.playerHealth);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        currentPlayerHealth -= damage;

        if (healthBar != null)
        {
            healthBar.SetHealth(currentPlayerHealth);
        }

        if (currentPlayerHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        playerDeathEffect.SetActive(true); 
        Time.timeScale = 0f; 
    }

    public void Heal(int healAmount)
    {
        // Add the health
        currentPlayerHealth += healAmount;

        // Prevent health from going over the maximum
        if (currentPlayerHealth > playerStats.playerHealth)
        {
            currentPlayerHealth = playerStats.playerHealth;
        }

        // Update the health bar UI if you have one connected
        if (healthBar != null)
        {
            healthBar.SetHealth(currentPlayerHealth);
        }
    }
}
