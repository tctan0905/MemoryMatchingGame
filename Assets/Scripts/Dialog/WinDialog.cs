using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinDialog : MonoBehaviour
{
   [SerializeField] private Button btnHome, btnRestart, btnNextLevel;
   
    int nextLevel; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btnHome.onClick.AddListener(PressHome);
        btnRestart.onClick.AddListener(PressRestart);
        btnNextLevel.onClick.AddListener(PressNextLevel);        
    }

    public void Init(int nextLevel)
    {
        this.nextLevel = nextLevel;
        if (DataManager.instance == null && LevelManager.instance == null)
            return;
        var dataManager = DataManager.instance;
        var levelManager = LevelManager.instance;
    }

    void PressHome()
    {
        SceneManager.LoadScene("MenuScene");
        
    }

    void PressRestart()
    {
        this.gameObject.SetActive(false);
    }

    void PressNextLevel()
    {
        this.gameObject.SetActive(false);
        GameEvents.OnNextLevel?.Invoke();
    }
}
