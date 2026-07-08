using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] RectTransform canvas, canvasUI;
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
        var settingDialogPrefab = Resources.Load<GameObject>("Prefabs/Dialog/" + Constant.DIALOG_SETTING);
        if (settingDialogPrefab != null)
        {
            var dialogSetting = Instantiate(settingDialogPrefab, canvasUI);
            var settingScript = dialogSetting.GetComponent<SettingDialog>();
            if (settingScript != null)
            {
                settingScript.Init();
            }
            else
            {
                Debug.LogError("SettingDialog script not found on the prefab.");
            }
        }
        else
        {
            Debug.LogError("SettingDialog prefab not found in Resources/Prefabs/Dialog.");
        }
    }

    public void QuitGame()
    {
        // Quit the application
        Debug.Log("Pressed Quit Game");
        Application.Quit();
    }
    
    public void OpenMenuLevelDialog()
    {
        // Load the level selection dialog
        Debug.Log("Pressed Level Dialog");
        var levelDialogPrefab = Resources.Load<GameObject>("Prefabs/Dialog/" + Constant.DIALOG_MENU_LEVEL);
        if (levelDialogPrefab != null)
        {
            var dialogMenuLevel = Instantiate(levelDialogPrefab, canvasUI);
            var menuLevelScript = dialogMenuLevel.GetComponent<MenuLevelDialog>();
            if (menuLevelScript != null)
            {
                menuLevelScript.Init();
            }
            else
            {
                Debug.LogError("MenuLevelDialog script not found on the prefab.");
            }
        }
        else
        {
            Debug.LogError("MenuLevelDialog prefab not found in Resources/Prefabs/Dialog.");
        }
    }
}
