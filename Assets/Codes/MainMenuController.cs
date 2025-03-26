using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles main menu navigation, allowing players to start a new game or continue a saved game.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    /// <summary>
    /// Starts a new game by loading the Level Selection scene.
    /// </summary>
    public void StartNewGame()
    {
        SceneManager.LoadScene("LevelSelectionScene");
    }

    /// <summary>
    /// Continues a game by loading the Save Game scene.
    /// </summary>
    public void ContinueGame()
    {
        SceneManager.LoadScene("SaveGameScene");
    }
}
