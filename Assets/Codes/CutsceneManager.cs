using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages the cutscene by displaying sequential story parts along with corresponding images,
/// and transitions to the next scene upon completion.
/// </summary>
public class CutsceneManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Text storyText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Image storyImage;

    [Header("Scene Settings")]
    [SerializeField] private string nextSceneName = "Level1Scene";

    // Array holding the story parts.
    private string[] storyParts;
    // Array holding the corresponding image names (should match file names in Resources).
    private string[] storyImageNames;
    // Current index in the story sequence.
    private int currentStoryIndex = 0;

    private void Start()
    {
        // Validate UI references.
        if (storyText == null || nextButton == null || storyImage == null)
        {
            Debug.LogError("One or more UI elements are not assigned in the inspector.");
            return;
        }

        // Define the story text.
        storyParts = new string[]
        {
            "Martha, a loving mother and wife, dedicated her life to homemaking the day she got married.",
            "Her heart is delighted as she watches the family grow. She made sure that her children are loved and nurtured.",
            "The cute little angels of hers made her occupied throughout the years and gave her purpose.",
            "But she was reminded that time is marching when her children grew up and are now training to be independent.",
            "The day all her children left for university, she immediately felt sorrowful despite the joy she felt for her children’s future.",
            "It’s just her and her husband again. But she still feels lonely as her husband has to go to work.",
            "But then, something caught her attention as she cried in their room. A box she kept all these years.",
            "The box that kept something significant that might give her fulfillment again. A diploma. She was reminded of her dream back then.",
            "This ignited her to apply for her dream job back then.",
            "The very next week, she immediately went to the private high school near their home to apply for the position of guidance counselor.",
            "There, she met the kind principal, Mr. James Robert Milne."
        };

        // Define corresponding story image names.
        storyImageNames = new string[]
        {
            "Panel1",
            "Panel2.3",
            "Panel3.1",
            "Panel3.2",
            "Panel4",
            "Panel5.1",
            "Panel5.2",
            "Panel6.1",
            "Panel6.2",
            "Panel8",
            "Panel9",
        };

        // Subscribe to the next button click event.
        nextButton.onClick.AddListener(DisplayNextStoryPart);

        // Display the first story part.
        DisplayNextStoryPart();
    }

    private void OnDestroy()
    {
        // Unsubscribe from the button event to avoid memory leaks.
        if (nextButton != null)
        {
            nextButton.onClick.RemoveListener(DisplayNextStoryPart);
        }
    }

    /// <summary>
    /// Displays the next story part along with its corresponding image. 
    /// If the end of the story is reached, the next scene is loaded.
    /// </summary>
    private void DisplayNextStoryPart()
    {
        if (currentStoryIndex < storyParts.Length)
        {
            // Set the story text for the current part.
            storyText.text = storyParts[currentStoryIndex];

            // Load and display the corresponding image if available.
            if (currentStoryIndex < storyImageNames.Length && !string.IsNullOrEmpty(storyImageNames[currentStoryIndex]))
            {
                Sprite loadedSprite = Resources.Load<Sprite>(storyImageNames[currentStoryIndex]);
                if (loadedSprite != null)
                {
                    storyImage.gameObject.SetActive(true);
                    storyImage.sprite = loadedSprite;
                }
                else
                {
                    Debug.LogWarning($"Sprite '{storyImageNames[currentStoryIndex]}' not found in Resources. Hiding image.");
                    storyImage.gameObject.SetActive(false);
                }
            }
            else
            {
                // Hide the image if no valid name is provided.
                storyImage.gameObject.SetActive(false);
            }

            currentStoryIndex++;
        }
        else
        {
            // End of cutscene reached; load the next scene.
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
