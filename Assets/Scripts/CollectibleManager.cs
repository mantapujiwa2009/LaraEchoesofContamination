using UnityEngine;
using TMPro; 

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;

    [Header("Settings")]
    public int collectedCount = 0;
    public int totalCollectibles = 7;

    [Header("UI Elements")]
    public TextMeshProUGUI counterText;
    public GameObject winPanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (winPanel != null) winPanel.SetActive(false);
        UpdateUI();
    }

    public void AddCollectible()
    {
        collectedCount++;
        UpdateUI();

        if (collectedCount >= totalCollectibles)
        {
            WinGame();
        }
    }

    void UpdateUI()
    {
        if (counterText != null)
        {
            counterText.text = collectedCount + " / " + totalCollectibles;
        }
    }

    void WinGame()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
        
        // Stop time completely
        Time.timeScale = 0f; 
        Debug.Log("You win! Time stopped.");
    }
}