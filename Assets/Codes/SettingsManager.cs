using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages game settings such as music volume and handles the opening/closing of the settings panel.
/// This script should be on an active GameObject (not on the settings panel itself) so that it starts immediately.
/// </summary>
public class SettingsManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject settingsPanel; // This panel can be toggled, but the SettingsManager must be active.
    [SerializeField] private Slider musicSlider;

    [Header("Audio")]
    [SerializeField] private AudioSource musicSource;

    private const string MusicVolumeKey = "MusicVolume";
    private const float DefaultMusicVolume = 1f;

    /// <summary>
    /// Awake is called when the script instance is loaded.
    /// Ensure the background music starts immediately.
    /// </summary>
    private void Awake()
    {
        if (musicSource == null)
        {
            Debug.LogError("SettingsManager: Music source is not assigned.");
        }
        else if (!musicSource.isPlaying)
        {
            // Start playing background music immediately.
            musicSource.PlayDelayed(0.1f);
        }
    }

    /// <summary>
    /// Initializes UI and loads saved volume settings.
    /// </summary>
    private void Start()
    {
        // Validate UI references.
        if (settingsPanel == null)
            Debug.LogError("SettingsManager: Settings panel is not assigned.");
        if (musicSlider == null)
            Debug.LogError("SettingsManager: Music slider is not assigned.");

        // Load saved volume settings.
        float savedMusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, DefaultMusicVolume);
        musicSlider.value = savedMusicVolume;

        // Apply the loaded volume settings.
        UpdateMusicVolume();

        // Add listener for slider value changes.
        musicSlider.onValueChanged.AddListener(_ => UpdateMusicVolume());

        // Hide the settings panel initially.
        settingsPanel.SetActive(false);
    }

    /// <summary>
    /// Updates the music volume based on the slider value and saves the setting.
    /// </summary>
    public void UpdateMusicVolume()
    {
        if (musicSource != null && musicSlider != null)
        {
            musicSource.volume = musicSlider.value;
            PlayerPrefs.SetFloat(MusicVolumeKey, musicSlider.value);
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// Opens the settings panel.
    /// </summary>
    public void OpenSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    /// <summary>
    /// Closes the settings panel.
    /// </summary>
    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }
}
