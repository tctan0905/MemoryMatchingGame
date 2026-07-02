using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverDialog : MonoBehaviour
{
    [SerializeField] private Button btnRestart; // Reference to the restart button
    [SerializeField] private Button btnHome; // Reference to the exit button
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btnRestart?.onClick.AddListener(PressRestart);
        btnHome?.onClick.AddListener(PressHome);
    }

    public void Init()
    {
        
    }

    void PressRestart()
    {
        Debug.Log("Restart button pressed.");
        this.gameObject.SetActive(false);
    }

    void PressHome()
    {
        Debug.Log("Home button pressed.");
        SceneManager.LoadScene("MenuScene");
    }
}
