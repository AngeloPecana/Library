using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Needed for scene switching

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public SpriteSpawner spriteSpawner; // Reference to the SpriteSpawner
    public Text scoreText;              // Reference to the UI Text component
    public Text timerText;              // Reference to the UI Text for timer
    public GameObject levelCompletePanel; // Reference to the Level Complete Panel
    public GameObject failPanel;        // Reference to the Fail Panel
    public Button continueButton;       // Reference to Continue Button
    public float levelTime = 60f;       // Time limit for the level (seconds)

    private int score = 0;              // The player's score
    private float timeRemaining;        // Timer countdown
    private bool isGameActive = true;   // Control to stop updates when game ends

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        timeRemaining = levelTime; // Initialize the timer
    }

    void Update()
    {
        if (isGameActive)
        {
            // Update the score UI
            scoreText.text = "Score: " + score;

            // Countdown timer
            timeRemaining -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Ceil(timeRemaining); // Display time left

            // If time runs out, show the fail screen
            if (timeRemaining <= 0)
            {
                GameOver();
            }
        }
    }

    public void AddPoints(int points)
    {
        if (!isGameActive) return; // Stop scoring if game is over

        score += points;

        // Prevent score from going negative
        if (score < 0)
        {
            score = 0;
        }

        // Check if the player reached the target score (20)
        if (score >= 20)
        {
            LevelComplete();
        }
    }

    private void LevelComplete()
    {
        isGameActive = false; // Stop updating the game
        Debug.Log("Level Complete!");

        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(true); // Show the Level Complete Panel
        }

        if (spriteSpawner != null)
        {
            spriteSpawner.StopSpawning();  // Stop the spawning of sprites
        }
    }

    private void GameOver()
    {
        isGameActive = false; // Stop updates when game over
        Debug.Log("Time's up! Level Failed.");

        if (failPanel != null)
        {
            failPanel.SetActive(true); // Show the Fail Panel
        }

        if (spriteSpawner != null)
        {
            spriteSpawner.StopSpawning();  // Stop sprite spawning
        }
    }

    // Unlock the next level when the player clicks Continue
    public void UnlockNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Unlock the next level if it hasn't been unlocked yet
        if (currentLevel >= unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentLevel + 1);
            PlayerPrefs.Save();
        }

        // Load the Level Selection screen
        SceneManager.LoadScene("LevelSelectionScene");
    }

    // Button Methods
    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart level
    }

    public void GoToLevelSelection()
    {
        SceneManager.LoadScene("LevelSelectionScene"); // Load Level Selection Scene
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Main Menu Scene
    }
}
