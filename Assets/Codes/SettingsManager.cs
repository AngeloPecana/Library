using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages game settings, such as music volume, and handles the opening and closing of the settings panel.
/// </summary>
public class SettingsManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider musicSlider;

    [Header("Audio")]
    [SerializeField] private AudioSource musicSource;

    private const string MusicVolumeKey = "MusicVolume";
    private const float DefaultMusicVolume = 1f;

    private void Start()
    {
        // Validate references.
        if (settingsPanel == null)
            Debug.LogError("SettingsManager: Settings panel is not assigned.");

        if (musicSlider == null)
            Debug.LogError("SettingsManager: Music slider is not assigned.");

        if (musicSource == null)
            Debug.LogError("SettingsManager: Music source is not assigned.");

        // Load saved volume settings.
        float savedMusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, DefaultMusicVolume);

        // Set the slider's value to the saved volume.
        musicSlider.value = savedMusicVolume;

        // Apply the loaded volume settings.
        UpdateMusicVolume();

        // Add listener for slider value changes.
        musicSlider.onValueChanged.AddListener(delegate { UpdateMusicVolume(); });

        // Play music on start if not already playing.
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.PlayDelayed(0.1f);
        }

        // Hide the settings panel initially.
        if (settingsPanel != null)
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
