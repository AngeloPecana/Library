using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider; // New SFX slider

    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume"; // Key for SFX volume
    private const float DefaultMusicVolume = 1f;
    private const float DefaultSFXVolume = 1f; // Default SFX volume

    private void Awake()
    {
        if (_settingsPanel == null)
        {
            Debug.LogError("SettingsManager: Settings panel is not assigned.");
        }
        if (_musicSlider == null)
        {
            Debug.LogError("SettingsManager: Music slider is not assigned.");
        }
        if (_sfxSlider == null)
        {
            Debug.LogError("SettingsManager: SFX slider is not assigned.");
        }
    }

    private void Start()
    {
        // Load saved volume settings.
        float savedMusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, DefaultMusicVolume);
        _musicSlider.value = savedMusicVolume;
        UpdateMusicVolume();

        float savedSFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey, DefaultSFXVolume);
        _sfxSlider.value = savedSFXVolume;
        UpdateSFXVolume();

        // Listen for slider value changes.
        _musicSlider.onValueChanged.AddListener(_ => UpdateMusicVolume());
        _sfxSlider.onValueChanged.AddListener(_ => UpdateSFXVolume());

        // Hide the settings panel initially.
        _settingsPanel.SetActive(false);
    }

    /// <summary>
    /// Updates the background music volume setting.
    /// </summary>
    public void UpdateMusicVolume()
    {
        if (_musicSlider != null)
        {
            float volume = _musicSlider.value;
            PlayerPrefs.SetFloat(MusicVolumeKey, volume);
            PlayerPrefs.Save();

            if (AudioManager.instance != null)
            {
                AudioManager.instance.SetVolume(volume);
            }
        }
    }

    /// <summary>
    /// Updates the SFX volume setting.
    /// </summary>
    public void UpdateSFXVolume()
    {
        if (_sfxSlider != null)
        {
            float volume = _sfxSlider.value;
            PlayerPrefs.SetFloat(SFXVolumeKey, volume);
            PlayerPrefs.Save();

            if (UIAudioManager.instance != null)
            {
                UIAudioManager.instance.SetSFXVolume(volume);
            }
        }
    }

    public void OpenSettings()
    {
        _settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        _settingsPanel.SetActive(false);
    }
}
