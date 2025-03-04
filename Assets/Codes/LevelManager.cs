using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button[] levelButtons;

    void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 <= unlockedLevel)
                levelButtons[i].interactable = true;
            else
                levelButtons[i].interactable = false;
        }
    }

    public void LoadLevel(int level)
{
    string sceneName = "";

    switch (level)
    {
        case 1:
            sceneName = "Level1Scene";
            break;
        case 2:
            sceneName = "Level2Scene";
            break;
        case 3:
            sceneName = "Level3Scene";
            break;
        case 4:
            sceneName = "Level4Scene";
            break;
        case 5:
            sceneName = "Level5Scene";
            break;
        default:
            Debug.LogError("Invalid level number: " + level);
            return;
    }

    SceneManager.LoadScene(sceneName);
}


    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void CompleteLevel(int level)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (level >= unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", level + 1);
            PlayerPrefs.Save();
        }
    }
}
