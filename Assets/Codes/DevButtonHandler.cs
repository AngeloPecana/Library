using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DevButtonHandler : MonoBehaviour
{
    public GameObject creditsPanel; // Assign the CreditsPanel in the Inspector

    private CanvasGroup canvasGroup;

    void Start()
    {
        if (creditsPanel != null)
        {
            // Ensure the panel starts hidden
            canvasGroup = creditsPanel.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = creditsPanel.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 0;
            creditsPanel.SetActive(false);
        }
    }

    public void OpenCredits()
    {
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(true);
            StartCoroutine(FadeInPanel());
        }
    }

    public void CloseCredits()
    {
        if (creditsPanel != null)
        {
            StartCoroutine(FadeOutPanel());
        }
    }

    IEnumerator FadeInPanel()
    {
        float duration = 0.5f; // Duration of fade
        float elapsed = 0f;
        canvasGroup.alpha = 0;
        creditsPanel.SetActive(true);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
    }

    IEnumerator FadeOutPanel()
    {
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = 1 - Mathf.Clamp01(elapsed / duration);
            yield return null;
        }

        creditsPanel.SetActive(false);
    }
}
