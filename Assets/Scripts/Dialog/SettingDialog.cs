using UnityEngine;
using UnityEngine.UI;

public class SettingDialog : MonoBehaviour
{
    [SerializeField] private Slider soundSlider, musicSlider;
    [SerializeField] private Button btnClose;
    void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soundSlider.value = DataManager.instance.SoundVolume;
        musicSlider.value = DataManager.instance.MusicVolume;
        soundSlider.onValueChanged.AddListener((value) => SoundSlider(value));
        musicSlider.onValueChanged.AddListener((value) => MusicSlider(value));

        btnClose.onClick.AddListener(PressClose);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init()
    {
        // Initialize the dialog if needed
        LoadVolume();
    }
    void SoundSlider(float value)
    {
        DataManager.instance.SoundVolume = value;
        DataManager.instance.SaveData();
    }

    void MusicSlider(float value)
    {
        DataManager.instance.MusicVolume = value;
        DataManager.instance.SaveData();
    }

    void LoadVolume()
    {
        soundSlider.value = DataManager.instance.SoundVolume;
        musicSlider.value = DataManager.instance.MusicVolume;
    }

    void PressClose()
    {
        Destroy(gameObject); // Close the dialog
    }
}
