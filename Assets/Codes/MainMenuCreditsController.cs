using System.Collections;
using UnityEngine;

public class MainMenuCreditsController : CreditsController
{
    [SerializeField] private GameObject _creditsPanel;

    private Coroutine _creditsCoroutine;

    public override void StartCredits()
    {
        if (_creditsCoroutine != null)
        {
            StopCoroutine(_creditsCoroutine);
        }
        _creditsCoroutine = StartCoroutine(RunCreditsSequence());
    }

    public void OpenCredits()
    {
        // Stop any running credits sequence before restarting
        if (_creditsCoroutine != null)
        {
            StopCoroutine(_creditsCoroutine);
            _creditsCoroutine = null;
        }

        // Ensure the panel is active
        if (_creditsPanel != null)
        {
            _creditsPanel.SetActive(true);
        }

        // Reset credits progress
        currentEntryIndex = 0;
        creditsText.text = "";
        canvasGroup.alpha = 0;

        StartCredits(); // Start fresh credits sequence
    }

    public void CloseCredits()
    {
        // Stop any ongoing credits sequence
        if (_creditsCoroutine != null)
        {
            StopCoroutine(_creditsCoroutine);
            _creditsCoroutine = null;
        }

        // Hide the panel
        if (_creditsPanel != null)
        {
            _creditsPanel.SetActive(false);
        }
    }
}
