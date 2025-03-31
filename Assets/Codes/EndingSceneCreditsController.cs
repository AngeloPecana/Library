using UnityEngine;

/// <summary>
/// Waits for the ending story images to finish, then activates the CreditsPanel
/// and starts the credits sequence.
/// </summary>
public class EndingSceneCreditsController : CreditsController
{
    [SerializeField] private EndingSceneStoryImageController _storyImageController;
    [SerializeField] private GameObject _creditsPanel;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _creditsPanel.SetActive(false);
        if (_storyImageController != null)
        {
            _storyImageController.OnStoryImagesFinished += OnStoryImagesFinishedHandler;
        }
        else
        {
            ActivateCredits();
        }
    }

    private void OnDestroy()
    {
        if (_storyImageController != null)
        {
            _storyImageController.OnStoryImagesFinished -= OnStoryImagesFinishedHandler;
        }
    }

    private void OnStoryImagesFinishedHandler()
    {
        ActivateCredits();
    }

    private void ActivateCredits()
    {
        _creditsPanel.SetActive(true);
        StartCredits();
    }
}