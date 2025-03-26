using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the logo scene by transitioning to the start menu after a delay.
/// </summary>
public class LogoSceneManager : MonoBehaviour
{
    [Tooltip("Delay (in seconds) before transitioning to the start menu.")]
    [SerializeField] private float delay;

    private void Start()
    {
        Invoke(nameof(LoadStartMenu), delay);
    }

    /// <summary>
    /// Loads the Start Menu scene.
    /// </summary>
    private void LoadStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
