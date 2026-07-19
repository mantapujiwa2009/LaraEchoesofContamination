using UnityEngine;
using UnityEngine.InputSystem; 

public class NPpcDialogur : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject pressEPrompt;
    public GameObject dialogueBox;

    private bool isPlayerInRange = false;
    private bool isDialogueActive = false;

    void Start()
    {
        pressEPrompt.SetActive(false);
        dialogueBox.SetActive(false);
    }

    void Update()
    {
        if (!isPlayerInRange) return;

        if (!isDialogueActive && Keyboard.current.eKey.wasPressedThisFrame)
        {
            isDialogueActive = true;
            pressEPrompt.SetActive(false); 
            dialogueBox.SetActive(true);   
        }
        else if (isDialogueActive && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            isDialogueActive = false;
            dialogueBox.SetActive(false);  
            pressEPrompt.SetActive(true);  
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            
            if (!isDialogueActive) 
            {
                pressEPrompt.SetActive(true); 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            isDialogueActive = false;
            
            pressEPrompt.SetActive(false);
            dialogueBox.SetActive(false);
        }
    }
}