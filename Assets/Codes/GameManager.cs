using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public SpriteSpawner spriteSpawner;
    public Text scoreText;
    public Text timerText;
    public GameObject levelCompletePanel;
    public GameObject failPanel;
    
    // Pause Menu References
    public GameObject pauseMenu;       // Reference to the Pause Menu Panel
    public Button pauseButton;         // Reference to the Pause Button
    public Button resumeButton;        // Reference to the Resume Button
    public Button leaveButton;         // Reference to the Leave Button

    public float levelTime = 60f;

    private int score = 0;
    private float timeRemaining;
    private bool isGameActive = true;
    private int targetScore;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        timeRemaining = levelTime;
        SetTargetScore();

        // Pause Menu Button Listeners
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        leaveButton.onClick.AddListener(GoToLevelSelection);
    }

    void Update()
    {
        if (isGameActive)
        {
            scoreText.text = "Score: " + score + " / " + targetScore;

            timeRemaining -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Ceil(timeRemaining);

            if (timeRemaining <= 0)
            {
                GameOver();
            }
        }
    }

    void SetTargetScore()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "Level1Scene": targetScore = 7; break;
            case "Level2Scene": targetScore = 10; break;
            case "Level3Scene": targetScore = 12; break;
            case "Level4Scene": targetScore = 15; break;
            case "Level5Scene": targetScore = 30; break;
            default: targetScore = 20; break;
        }
    }

    public void AddPoints(int points)
    {
        if (!isGameActive) return;

        score += points;

        if (score < 0)
            score = 0;

        if (score >= targetScore)
            LevelComplete();
    }

    private void GameOver()
    {
        isGameActive = false;
        Debug.Log("Time's up! Level Failed.");

        if (failPanel != null)
            failPanel.SetActive(true);

        if (spriteSpawner != null)
            spriteSpawner.StopSpawning();
    }

    // 🔹 Pause Menu Functions 🔹
    public void PauseGame()
    {
        isGameActive = false;        // Stop game updates
        Time.timeScale = 0f;         // Pause the game
        pauseMenu.SetActive(true);   // Show Pause Menu
    }

    public void ResumeGame()
    {
        isGameActive = true;         // Resume game updates
        Time.timeScale = 1f;         // Resume the game
        pauseMenu.SetActive(false);  // Hide Pause Menu
    }

    public void GoToLevelSelection()
    {
        Time.timeScale = 1f;         // Ensure the game time resumes
        SceneManager.LoadScene("LevelSelectionScene");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
}
