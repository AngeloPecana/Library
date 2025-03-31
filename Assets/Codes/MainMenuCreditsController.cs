using System;
using System.Collections;
using UnityEngine;

public class MainMenuCreditsController : CreditsController
{
    [SerializeField] private GameObject _creditsPanel;

    private Coroutine _creditsCoroutine;

    public event Action OnCreditsFinished;

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
        if (_creditsPanel != null)
        {
            _creditsPanel.SetActive(true);
        }
        StartCredits();
    }
}
