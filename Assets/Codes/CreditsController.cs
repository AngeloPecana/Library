using UnityEngine;

/// <summary>
/// Controls the visibility of the Credits panel.
/// </summary>
public class CreditsController : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    private void Start()
    {
        if (_panel == null)
        {
            Debug.LogError("CreditsController: Panel is not assigned.");
            return;
        }

        _panel.SetActive(false);
    }

    /// <summary>
    /// Opens (activates) the credits panel.
    /// </summary>
    public void Open()
    {
        if (_panel == null) return;

        _panel.SetActive(true);
    }

    /// <summary>
    /// Closes (deactivates) the credits panel.
    /// </summary>
    public void Close()
    {
        if (_panel == null) return;

        _panel.SetActive(false);
    }
}
