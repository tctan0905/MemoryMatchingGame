using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour
{
    private LevelData levelData;
    private Button btnLevel;
    private Image imgDark;
    [SerializeField] private Text txtLevel;
    
    void  Awake()
    {
        btnLevel = this.transform.Find("btn_level").GetComponent<Button>();
        imgDark = this.transform.Find("img_dark").GetComponent<Image>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btnLevel = this.transform.Find("btn_level").GetComponent<Button>();
        if (btnLevel != null)
        {
            btnLevel.onClick.AddListener(OnLevelItemClicked);
        }
    }

    private void OnLevelItemClicked()
    {
        if (LevelManager.instance == null || levelData == null)
        {
            Debug.LogError("LevelManager instance or levelData is null.");
            return;
        }
        {
            var levelManager = LevelManager.instance;
            levelManager.GetLevelByIndex(levelData.Level - 1);
        }
        SceneManager.LoadScene("GameScene");
    }

    public void Init(LevelData levelData)
    {
        if (LevelManager.instance == null && DataManager.instance == null)
            return;
        var levelManager = LevelManager.instance;
        var dataManager = DataManager.instance;
        this.levelData = levelData;
        if (levelData == null)
        {
            Debug.LogError("Level data not found for level: " + levelData.Level);
            return;
        }
        txtLevel.text = "Level\n" + levelData.Level;
        SetLocked(dataManager.CurrentLevel + 1 < levelData.Level);
    }

    public void SetLocked(bool isLocked)
    {
        if (imgDark != null)
        {
            imgDark.gameObject.SetActive(isLocked);
        }
    }
}
