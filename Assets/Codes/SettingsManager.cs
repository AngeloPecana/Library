using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider musicSlider;
    public AudioSource musicSource;

    void Start()
    {
        // Load saved volume settings
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);

        // Set slider values
        musicSlider.value = savedMusicVolume;

        // Apply volume settings
        UpdateMusicVolume();

        // Add listeners for volume changes
        musicSlider.onValueChanged.AddListener(delegate { UpdateMusicVolume(); });

        // Play music on start if not already playing
        if (!musicSource.isPlaying)
        {
            musicSource.PlayDelayed(0.1f);

        }

        // Hide settings panel initially
        settingsPanel.SetActive(false);
    }

    public void UpdateMusicVolume()
    {
        musicSource.volume = musicSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.Save();
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
}
