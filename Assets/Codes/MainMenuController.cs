using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("LevelSelectionScene");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("SaveGameScene");
    }
}
