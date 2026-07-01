using UnityEngine;

public class MenuController : MonoBehaviour
{

    public void StartGame()
    {
        // Load the game scene
        Debug.Log("Pressed Start Game");
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void SettingGame()
    {
        // Load the settings scene
        Debug.Log("Pressed Setting Game");
    }

    public void QuitGame()
    {
        // Quit the application
        Debug.Log("Pressed Quit Game");
        Application.Quit();
    }
}
