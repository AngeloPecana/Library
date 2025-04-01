using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip _mainMenuBGM;
    [SerializeField] private AudioClip _levelBGM;
    [SerializeField] private AudioClip _cutsceneBGM;

    [Header("Audio Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float _volume = 1f;

    private AudioSource _audioSource;
    private string _currentScene = "";

    private void Awake()
    {
        // Singleton pattern: Ensure a single instance persists across scenes.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Get or add an AudioSource.
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
        _audioSource.loop = true;
        _audioSource.volume = _volume;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string newScene = scene.name;

        // Keep music state if switching between MainMenu and LevelSelection
        if ((_currentScene == "MainMenuScene" || _currentScene == "LevelSelectionScene") &&
            (newScene == "MainMenuScene" || newScene == "LevelSelectionScene"))
        {
            _currentScene = newScene;
            return; // Don't reset the music
        }

        // Determine the appropriate BGM
        AudioClip newClip = null;
        if (newScene == "MainMenuScene" || newScene == "LevelSelectionScene")
        {
            newClip = _mainMenuBGM;
        }
        else if (newScene.StartsWith("Level"))
        {
            newClip = _levelBGM;
        }
        else if (newScene == "CutsceneScene")
        {
            newClip = _cutsceneBGM;
        }

        // Play only if there's a new valid clip
        if (newClip != null && _audioSource.clip != newClip)
        {
            _audioSource.clip = newClip;
            _audioSource.Play();
        }
        else if (newClip == null)
        {
            _audioSource.Stop(); // Stop playing if no music is assigned
        }

        _currentScene = newScene;
    }

    /// <summary>
    /// Sets the volume for the background music.
    /// </summary>
    public void SetVolume(float vol)
    {
        _volume = vol;
        if (_audioSource != null)
        {
            _audioSource.volume = _volume;
        }
    }
}


