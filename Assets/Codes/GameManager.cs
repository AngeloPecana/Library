using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        // Ensure a single instance of GameManager exists.
        if (instance == null)
        {
            instance = this;
            // Optionally persist across scenes:
            // DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    [Header("Gameplay References")]
    [SerializeField] private SpriteSpawner spriteSpawner;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timerText;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject failPanel;

    [Header("Pause Menu References")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button leaveButton;

    [Header("Level Settings")]
    [SerializeField] private float levelTime = 60f;

    private int score = 0;
    private float timeRemaining;
    private bool isGameActive = true;
    private int targetScore;

    /// <summary>
    /// Public property to check if the game is currently active (not paused).
    /// </summary>
    public bool IsGameActive => isGameActive;

    private void Start()
    {
        // Validate critical UI references.
        if (scoreText == null || timerText == null)
            Debug.LogError("GameManager: ScoreText or TimerText is not assigned.");

        timeRemaining = levelTime;
        SetTargetScore();

        // Register button listeners.
        if (pauseButton != null)
            pauseButton.onClick.AddListener(PauseGame);
        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);
        if (leaveButton != null)
            leaveButton.onClick.AddListener(GoToLevelSelection);
    }

    private void Update()
    {
        if (isGameActive)
        {
            UpdateScoreDisplay();
            UpdateTimer();

            if (timeRemaining <= 0)
                GameOver();
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe listeners to prevent memory leaks.
        if (pauseButton != null)
            pauseButton.onClick.RemoveListener(PauseGame);
        if (resumeButton != null)
            resumeButton.onClick.RemoveListener(ResumeGame);
        if (leaveButton != null)
            leaveButton.onClick.RemoveListener(GoToLevelSelection);
    }

    #region Game Flow Methods

    /// <summary>
    /// Sets the target score based on the active scene's name.
    /// </summary>
    private void SetTargetScore()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        targetScore = sceneName switch
        {
            "Level1Scene" => 10,
            "Level2Scene" => 15,
            "Level3Scene" => 20,
            "Level4Scene" => 25,
            "Level5Scene" => 30,
            _ => 20,
        };
    }

    /// <summary>
    /// Adds points to the current score and checks for level completion.
    /// </summary>
    /// <param name="points">Points to add (or subtract).</param>
    public void AddPoints(int points)
    {
        if (!isGameActive)
            return;

        score += points;
        if (score < 0)
            score = 0;

        if (score >= targetScore)
            LevelComplete();
    }

    /// <summary>
    /// Handles game over conditions when time runs out.
    /// </summary>
    private void GameOver()
    {
        isGameActive = false;
        Debug.Log("Time's up! Level Failed.");

        if (failPanel != null)
            failPanel.SetActive(true);

        if (spriteSpawner != null)
            spriteSpawner.StopSpawning();
    }

    /// <summary>
    /// Marks the level as complete and stops gameplay updates.
    /// </summary>
    private void LevelComplete()
    {
        isGameActive = false;
        Debug.Log("Level Complete!");

        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(true);

        if (spriteSpawner != null)
            spriteSpawner.StopSpawning();
    }

    #endregion

    #region UI & Timer Updates

    /// <summary>
    /// Updates the score display UI.
    /// </summary>
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score} / {targetScore}";
    }

    /// <summary>
    /// Updates the timer display and decreases the remaining time.
    /// </summary>
    private void UpdateTimer()
    {
        timeRemaining -= Time.deltaTime;
        if (timerText != null)
            timerText.text = $"Time: {Mathf.Ceil(timeRemaining)}";
    }

    #endregion

    #region Pause Menu Methods

    /// <summary>
    /// Pauses the game by stopping updates and freezing the time scale.
    /// </summary>
    public void PauseGame()
    {
        if (!isGameActive)
            return;
        isGameActive = false;
        Time.timeScale = 0f;
        if (pauseMenu != null)
            pauseMenu.SetActive(true);
    }

    /// <summary>
    /// Resumes the game by restarting updates and restoring the time scale.
    /// </summary>
    public void ResumeGame()
    {
        isGameActive = true;
        Time.timeScale = 1f;
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }

    /// <summary>
    /// Loads the Level Selection scene and ensures the time scale is reset.
    /// </summary>
    public void GoToLevelSelection()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelectionScene");
    }

    /// <summary>
    /// Loads the Main Menu scene.
    /// </summary>
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    #endregion

    #region Level Unlocking

    /// <summary>
    /// Unlocks the next level and loads the Level Selection screen.
    /// </summary>
    public void UnlockNextLevel()
    {
        int levelNumber = FindObjectOfType<LevelIdentifier>().levelNumber;

        // If the level 5 is completed, load the ending scene.
        if (levelNumber == 5)
        {
            SceneManager.LoadScene("EndingScene");
            return;
        }

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (levelNumber == unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", levelNumber + 1);
            PlayerPrefs.Save();
        }

        SceneManager.LoadScene("LevelSelectionScene");
    }

    #endregion

    /// <summary>
    /// Reloads the current level to restart the game.
    /// </summary>
    public void RetryLevel()
    {
        Time.timeScale = 1f; // Ensure time scale is reset in case of pause
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
