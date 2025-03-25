using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public Text storyText;
    public Button nextButton;
    public Image storyImage;
    public string nextSceneName = "Level1Scene";

    private string[] storyParts;
    private string[] storyImageNames;
    private int currentStoryIndex = 0;

    void Start()
    {
        // Define story text
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

        // Define corresponding story images (name must match file names in Resources)
        storyImageNames = new string[]
        {
            "Panel1",   // For story part 1
            "Panel2.3",    // For story part 2
            "Panel3.1",   // For story part 3
            "Panel3.2",      // For story part 4
            "Panel4",    // For story part 5
            "Panel5.1",   // For story part 6
            "Panel5.2",     // For story part 7
            "Panel6.1",    // For story part 8
            "Panel6.2",   // For story part 9
            "Panel8",   // For story part 10
            "Panel9",   // For story part 11
        };

        DisplayNextStoryPart();
        nextButton.onClick.AddListener(DisplayNextStoryPart);
    }

    void DisplayNextStoryPart()
    {
        if (currentStoryIndex < storyParts.Length)
        {
            // Set story text
            storyText.text = storyParts[currentStoryIndex];

            // Load and display the corresponding image (if available)
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
                    storyImage.gameObject.SetActive(false);
                }
            }
            else
            {
                storyImage.gameObject.SetActive(false);
            }

            currentStoryIndex++;
        }
        else
        {
            // End of cutscene → load the next scene
            SceneManager.LoadScene(nextSceneName);
        }
    }
}

