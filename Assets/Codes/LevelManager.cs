using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages level selection, button interactability based on unlocked levels,
/// and handles scene transitions for level loading and navigation.
/// </summary>
public class LevelManager : MonoBehaviour
{
    [Header("Level Selection UI")]
    [SerializeField] private Button[] levelButtons;

    private void Start()
    {
        if (levelButtons == null || levelButtons.Length == 0)
        {
            Debug.LogError("LevelManager: Level buttons are not assigned.");
            return;
        }

        // Retrieve the highest unlocked level (default is 1)
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Enable buttons for unlocked levels; disable for locked ones.
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = (i + 1) <= unlockedLevel;
        }
    }

    /// <summary>
    /// Loads the specified level based on the level number.
    /// </summary>
    /// <param name="level">The level number to load.</param>
    public void LoadLevel(int level)
    {
        string sceneName = level switch
        {
            1 => "CutsceneScene",
            2 => "Level2Scene",
            3 => "Level3Scene",
            4 => "Level4Scene",
            5 => "Level5Scene",
            _ => GetInvalidLevelString(level)
        };

        if (!string.IsNullOrEmpty(sceneName))
            SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Loads the main menu scene.
    /// </summary>
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Completes the specified level and unlocks the next level if not already unlocked.
    /// </summary>
    /// <param name="level">The level number that was completed.</param>
    public static void CompleteLevel(int level)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (level >= unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", level + 1);
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// Handles invalid level numbers in the switch expression.
    /// Logs an error and returns an empty string.
    /// </summary>
    /// <param name="level">The invalid level number.</param>
    /// <returns>An empty string.</returns>
    private static string GetInvalidLevelString(int level)
    {
        Debug.LogError($"Invalid level number: {level}");
        return string.Empty;
    }
}
