using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    public static UIAudioManager instance;

    [Header("UI Sound Effects")]
    [SerializeField] private AudioClip _buttonClickSFX;
    [SerializeField] private AudioClip _hoverSFX; // Optional: For hover effects

    [Header("SFX Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float _sfxVolume = 1f;

    private AudioSource _sfxSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _sfxSource = gameObject.AddComponent<AudioSource>();
        _sfxSource.loop = false;
        _sfxSource.volume = _sfxVolume;
    }

    public void PlayButtonClick()
    {
        if (_buttonClickSFX != null)
            _sfxSource.PlayOneShot(_buttonClickSFX, _sfxVolume);
    }

    public void PlayHoverSound()
    {
        if (_hoverSFX != null)
            _sfxSource.PlayOneShot(_hoverSFX, _sfxVolume);
    }

    public void SetSFXVolume(float vol)
    {
        _sfxVolume = vol;
        _sfxSource.volume = _sfxVolume;
    }
}
