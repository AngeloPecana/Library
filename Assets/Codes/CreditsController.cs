using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Displays a sequence of credit entries that fade in, hold, and fade out.
/// After all entries are shown, loads the next scene if specified.
/// </summary>
public class CreditsController : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] protected Text creditsText;

    [Header("Fade Settings")]
    [SerializeField] private float _fadeInDuration = 1f;
    [SerializeField] private float _holdDuration = 2f;
    [SerializeField] private float _fadeOutDuration = 1f;

    [Header("Credits Content")]
    [SerializeField] private string[] _creditEntries;

    [Header("Scene Settings")]
    [SerializeField] private string _nextSceneName = "";

    // Internal index for tracking the current credit entry.
    protected int currentEntryIndex = 0;
    // CanvasGroup used for fading the text.
    protected CanvasGroup canvasGroup;

    protected virtual void Awake()
    {
        canvasGroup = creditsText.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = creditsText.gameObject.AddComponent<CanvasGroup>();
        }
    }

    /// <summary>
    /// Starts the credits sequence.
    /// </summary>
    public virtual void StartCredits()
    {
        canvasGroup.alpha = 0;
        StartCoroutine(RunCreditsSequence());
    }

    protected IEnumerator RunCreditsSequence()
    {
        while (currentEntryIndex < _creditEntries.Length)
        {
            creditsText.text = _creditEntries[currentEntryIndex];

            yield return StartCoroutine(FadeCanvasGroup(0f, 1f, _fadeInDuration));
            yield return new WaitForSeconds(_holdDuration);
            yield return StartCoroutine(FadeCanvasGroup(1f, 0f, _fadeOutDuration));

            currentEntryIndex++;
        }

        if (!string.IsNullOrEmpty(_nextSceneName))
        {
            SceneManager.LoadScene(_nextSceneName);
        }
    }

    private IEnumerator FadeCanvasGroup(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }
}