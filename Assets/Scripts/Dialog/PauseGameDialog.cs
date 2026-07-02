using UnityEngine;
using UnityEngine.UI;
public class PauseGameDialog : MonoBehaviour
{
    [SerializeField] private Button btnResume;
    [SerializeField] private Button btnHome;

    void Start()
    {
        btnResume.onClick.AddListener(PressResume);
        btnHome.onClick.AddListener(PressHome);
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
        // Implement quit logic here, e.g., load main menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
