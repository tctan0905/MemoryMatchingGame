using UnityEngine;
using UnityEngine.UI;
public class PauseGameDialog : MonoBehaviour
{
    [SerializeField] private Button btnResume,btnHome,btnRetry;

    void Start()
    {
        btnResume.onClick.AddListener(PressResume);
        btnHome.onClick.AddListener(PressHome);
        btnRetry.onClick.AddListener(Pressretry);
    }
    public void Init()
    {
        // Initialize the dialog if needed
        Time.timeScale = 0f; // Pause the game
    }
    
    void PressResume()
    {
        Time.timeScale = 1f; // Resume the game
        Destroy(gameObject); // Close the dialog
    }

    void PressHome()
    {
        Time.timeScale = 1f; // Resume the game before quitting
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }

    void Pressretry()
    {
        Time.timeScale = 1f; // Resume the game before retrying
        GameEvents.OnNextLevel?.Invoke();
        Destroy(gameObject); // Close the dialog
    }
}
