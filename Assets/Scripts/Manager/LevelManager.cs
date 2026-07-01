using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private LevelScriptable levelScriptable;
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

}
