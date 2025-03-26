using TMPro;
using UnityEngine;

public class DropdownSample : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI outputText = null;
    [SerializeField] private TMP_Dropdown dropdownWithoutPlaceholder = null;
    [SerializeField] private TMP_Dropdown dropdownWithPlaceholder = null;

    /// <summary>
    /// Called when the associated button is clicked.
    /// Updates the output text based on the selected values from the dropdowns.
    /// </summary>
    public void OnButtonClick()
    {
        // Validate required references.
        if (outputText == null || dropdownWithoutPlaceholder == null || dropdownWithPlaceholder == null)
        {
            Debug.LogError("DropdownSample: One or more UI references are not assigned.");
            return;
        }

        // Check if a valid selection is made on the dropdown with placeholder.
        if (dropdownWithPlaceholder.value > -1)
        {
            outputText.text = $"Selected values:\n{dropdownWithoutPlaceholder.value} - {dropdownWithPlaceholder.value}";
        }
        else
        {
            outputText.text = "Error: Please make a selection";
        }
    }
}
