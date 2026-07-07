using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLevelDialog : MonoBehaviour
{
    [SerializeField] private GameObject levelItemPrefab;
    [SerializeField] private List<LevelItem> listLevelItem;
    [SerializeField] private Transform contentBoard;

    void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    public void Init()
    {
        if(DataManager.instance == null && LevelManager.instance == null)
           return;
        
        var dataManager = DataManager.instance;
        var levelManager = LevelManager.instance;

        if (levelManager.levelScriptable != null && levelManager.levelScriptable.levelDatas.Count > 0)
        {
            for (int i = 0; i < levelManager.levelScriptable.levelDatas.Count; i++)
            {
                var levelData = levelManager.levelScriptable.levelDatas[i];
                CreateLevelItems(levelData);
            }
        }
        else
        {
            Debug.LogError("No level data found.");
            return;
        }
    }

    void CreateLevelItems(LevelData levelData)
    {
        if (levelItemPrefab == null)
        {
            Debug.LogError("Level item prefab is not assigned.");
            return;
        }

        if(listLevelItem != null && listLevelItem.Count > 0)
        {
            foreach (var levelItem in listLevelItem)
            {
                if (!levelItem.gameObject.activeSelf)
                {
                    levelItem.gameObject.SetActive(true);
                    levelItem.Init(levelData);
                    return;
                }
            }
        }

        var levelPrefab = Instantiate(levelItemPrefab, contentBoard);
        var levelItemScript = levelPrefab.GetComponent<LevelItem>();
        if (levelItemScript != null)
        {
            levelItemScript.Init(levelData);
            listLevelItem.Add(levelItemScript);
        }
        else
        {
            Debug.LogError("Level item prefab does not have a LevelItem component.");
        }
    }
    
    public void PressCloseDialog()
    {
        Destroy(gameObject);
    }

    public void PressLevelItem(int level)
    {
        if(DataManager.instance == null && LevelManager.instance == null)
            return;

        Debug.Log("Selected Level: " + level);
        LevelManager.instance.GetLevelByIndex(level - 1); // Assuming level is 1-based index
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    void OnDisable()
    {
        foreach (var levelItem in listLevelItem)
        {
            if (levelItem != null)
            {
                levelItem.gameObject.SetActive(false);
            }
        }
    }
}
