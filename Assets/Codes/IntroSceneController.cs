using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the intro scene, allowing the player to proceed to the main menu after a short delay.
/// </summary>
public class IntroSceneController : MonoBehaviour
{
    private readonly float delay = 1f;

    // Flag to determine when input is allowed.
    private bool canProceed = false;

    /// <summary>
    /// Initializes the intro scene and schedules the input enabling.
    /// </summary>
    private void Start()
    {
        // Delay input enabling for 1 second.
        Invoke(nameof(EnableInput), delay);
    }

    /// <summary>
    /// Enables input so that the player can proceed to the main menu.
    /// </summary>
    private void EnableInput()
    {
        canProceed = true;
    }

    /// <summary>
    /// Checks for user input once input is allowed and loads the main menu scene.
    /// </summary>
    private void Update()
    {
        if (canProceed && (Input.GetMouseButtonDown(0) || Input.anyKeyDown))
        {
            LoadMainMenu();
        }
    }

    /// <summary>
    /// Loads the main menu scene.
    /// </summary>
    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
