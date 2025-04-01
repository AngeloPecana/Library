using UnityEngine;
using UnityEngine.UI;

public class UIButtonSFX : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void Start()
    {
        if (_button == null) return;
        _button.onClick.AddListener(() => UIAudioManager.instance.PlayButtonClick());
    }
}
