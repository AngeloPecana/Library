using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Fades a series of images in and out sequentially in the ending scene.
/// When all images have been displayed, fires an event to signal completion.
/// </summary>
public class EndingSceneStoryImageController : MonoBehaviour
{
    [Header("UI Components")]
    [Tooltip("The Image component that displays the ending images.")]
    [SerializeField] private Image _displayImage;

    [Header("Fade Settings")]
    [SerializeField] private float _fadeInDuration = 1f;
    [SerializeField] private float _holdDuration = 2f;
    [SerializeField] private float _fadeOutDuration = 1f;

    [Header("Image Series")]
    [SerializeField] private Sprite[] _imageSeries;

    private int _currentImageIndex = 0;
    private CanvasGroup _canvasGroup;

    /// <summary>
    /// Event fired when all story images have finished playing.
    /// </summary>
    public event System.Action OnStoryImagesFinished;

    private void Awake()
    {
        _canvasGroup = _displayImage.GetComponent<CanvasGroup>();
        if (_canvasGroup == null)
        {
            _canvasGroup = _displayImage.gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void Start()
    {
        _canvasGroup.alpha = 0;
        StartCoroutine(FadeSequence());
    }

    private IEnumerator FadeSequence()
    {
        while (_currentImageIndex < _imageSeries.Length)
        {
            _displayImage.sprite = _imageSeries[_currentImageIndex];

            yield return StartCoroutine(FadeCanvasGroup(0f, 1f, _fadeInDuration));
            yield return new WaitForSeconds(_holdDuration);
            yield return StartCoroutine(FadeCanvasGroup(1f, 0f, _fadeOutDuration));

            _currentImageIndex++;
        }

        OnStoryImagesFinished?.Invoke();
    }

    private IEnumerator FadeCanvasGroup(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }
        _canvasGroup.alpha = endAlpha;
    }
}
