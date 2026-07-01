using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    
    private int currentLevel;
    public int CurrentLevel {get => currentLevel; set => currentLevel = value;}
    void Awake()
    {
        // Ensure that the GameManager persists across scene loads
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveData()
    {
        //PlayerPrefs.SetInt("LevelKey", currentLevel);
        //PlayerPrefs.Save();
    }

    public void LoadData()
    {
        // if (PlayerPrefs.HasKey("LevelKey"))
        // {
        //     currentLevel = PlayerPrefs.GetInt("LevelKey", 0);

        //     Debug.Log($"Load Success {currentLevel} ");
        // }
        // else
        // {
        //     Debug.Log("Không tìm thấy file lưu. Đây là lần đầu tiên chơi game!");
        // }
    }
}
