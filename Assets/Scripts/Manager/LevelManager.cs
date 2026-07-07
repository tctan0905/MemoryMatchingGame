using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] public LevelScriptable levelScriptable;
    private int currentLevelIndex = 0;
    public int CurrentLevelIndex {get => currentLevelIndex; set => currentLevelIndex = value;}
    private LevelData currentLevelData;
    public LevelData CurrentLevelData => currentLevelData;
    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public LevelData GetCurrentLevel()
    {
        if(DataManager.instance == null) return null;
        var currentLevel = DataManager.instance.CurrentLevel;
        if(currentLevel >= levelScriptable.levelDatas.Count -1)
            return levelScriptable.levelDatas[^1];
        else
            return levelScriptable.levelDatas[currentLevel];
    }

    public LevelData GetLevelByIndex(int index)
    {
        if (index < 0 || index >= levelScriptable.levelDatas.Count)
        {
            Debug.LogError("Invalid level index: " + index);
            return null;
        }
        currentLevelData = levelScriptable.levelDatas[index];
        currentLevelIndex = index;
        return currentLevelData;
    }
}
