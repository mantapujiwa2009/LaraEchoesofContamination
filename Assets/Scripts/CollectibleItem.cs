using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player touched it
        if (other.CompareTag("Player"))
        {
            if (CollectibleManager.Instance != null)
            {
                CollectibleManager.Instance.AddCollectible();
            }

            Destroy(gameObject);
        }
    }
}