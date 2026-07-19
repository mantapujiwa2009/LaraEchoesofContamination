using UnityEngine;
using UnityEngine.SceneManagement; 

public class NextStage : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Simply load Scene 2
            SceneManager.LoadScene("Hutan");
        }
    }
}