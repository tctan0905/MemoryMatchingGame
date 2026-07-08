using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    
    private int currentLevel;
    public int CurrentLevel {get => currentLevel; set => currentLevel = value;}
    private float soundVolume = 1.0f;
    public float SoundVolume { get => soundVolume; set => soundVolume = value; }
    private float musicVolume = 1.0f;
    public float MusicVolume { get => musicVolume; set => musicVolume = value; }

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

    void Start()
    {
        //RemoveAllData();
        LoadData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("LevelKey", currentLevel);
        
        PlayerPrefs.SetFloat("SoundVolumeKey", soundVolume);
        PlayerPrefs.SetFloat("MusicVolumeKey", musicVolume);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("LevelKey"))
        {
            currentLevel = PlayerPrefs.GetInt("LevelKey", 0);

            Debug.Log($"Load Success {currentLevel} ");
        }
        else
        {
            Debug.Log("Không tìm thấy file lưu. Đây là lần đầu tiên chơi game!");
        }

        if (PlayerPrefs.HasKey("SoundVolumeKey"))
        {
            soundVolume = PlayerPrefs.GetFloat("SoundVolumeKey", 1.0f);
        }

        if (PlayerPrefs.HasKey("MusicVolumeKey"))
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolumeKey", 1.0f);
        }
    }

    public void RemoveAllData()
    {
        // PlayerPrefs.DeleteAll();
        // Debug.Log("All data removed.");
    }
}
