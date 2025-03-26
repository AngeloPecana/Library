using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DevButtonHandler : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject creditsPanel; // Assigned in Inspector

    private CanvasGroup canvasGroup;
    private Coroutine fadeCoroutine;
    private const float fadeDuration = 0.5f;

    void Awake()
    {
        if (creditsPanel != null)
        {
            canvasGroup = creditsPanel.GetComponent<CanvasGroup>() ?? creditsPanel.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            creditsPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("DevButtonHandler: Credits panel is not assigned!");
        }
    }

    public void OpenCredits()
    {
        if (creditsPanel == null || canvasGroup == null) return;

        creditsPanel.SetActive(true);
        StartFade(1);
    }

    public void CloseCredits()
    {
        if (creditsPanel == null || canvasGroup == null) return;

        StartFade(0);
    }

    private void StartFade(float targetAlpha)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadePanel(targetAlpha));
    }

    private IEnumerator FadePanel(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            yield return new WaitForEndOfFrame();
        }

        canvasGroup.alpha = targetAlpha;

        if (targetAlpha == 0)
            creditsPanel.SetActive(false);
    }
}
