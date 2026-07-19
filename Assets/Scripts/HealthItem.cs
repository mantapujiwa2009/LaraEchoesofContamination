using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healAmount = 20; // How much health this item gives

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that touched the item is the Player
        if (other.CompareTag("Player"))
        {
            // Find the PlayerManager script on the player
            PlayerManager playerManager = other.GetComponent<PlayerManager>();

            if (playerManager != null)
            {
                // Heal the player
                playerManager.Heal(healAmount);

                // Destroy the item so it can't be picked up twice
                Destroy(gameObject);
            }
        }
    }
}