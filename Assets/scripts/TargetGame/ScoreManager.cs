using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;



public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public bool gameOver = false; 

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI messageText;

    [Header("End Game Buttons")]
    public GameObject playAgainButton;
    public GameObject nextGameButton;

    [Header("Gameplay")]
    public Target target;

    [Header("Settings")]
    public int scorePerLevel = 10;
    public int maxLevel = 4;

    private int score;
    private int level = 1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        UpdateUI();

        // Hide end-game buttons at start
        if (playAgainButton != null)
            playAgainButton.SetActive(false);

        if (nextGameButton != null)
            nextGameButton.SetActive(false);
    }

    public void AddScore(int amount)
    {
        if (gameOver) return; // ⛔ STOP EVERYTHING

        score += amount;

        if (score >= level * scorePerLevel)
        {
            CompleteLevel();
        }

        UpdateUI();
    }

    private void CompleteLevel()
    {
        // 🔊 PLAY SOUND FOR CURRENT LEVEL (before increment)
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayLevelUp(level);
        }

        ShowLevelMessage(level);

        // 🏁 FINAL LEVEL
        if (level >= maxLevel)
        {
            gameOver = true;
            ShowWinMessage();

            if (GameTimer.Instance != null)
                GameTimer.Instance.StopTimer();

            if (target != null)
                target.gameObject.SetActive(false);

            if (playAgainButton != null)
                playAgainButton.SetActive(true);

            if (nextGameButton != null)
                nextGameButton.SetActive(true);

            UpdateUI();
            return;
        }

        // ➕ MOVE TO NEXT LEVEL
        level++;

        if (target != null)
            target.UpdateSizeForLevel(level);

        if (GameTimer.Instance != null)
            GameTimer.Instance.ResetTimer(30);

        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = $"Score: {score}";
        levelText.text = $"Level {level}";
    }

    private void ShowLevelMessage(int completedLevel)
    {
        messageText.gameObject.SetActive(true);

        string rating =
            completedLevel == 1 ? "GOOD " :
            completedLevel == 2 ? "GREAT " :
            completedLevel == 3 ? "EXCELLENT " :
            "AWESOME ";

        messageText.text =
            $"LEVEL {completedLevel} COMPLETE\n{rating}";

        CancelInvoke();
        Invoke(nameof(HideMessage), 2f);
    }

    private void ShowWinMessage()
    {
        gameOver = true;

        messageText.gameObject.SetActive(true);
        messageText.text = "YOU WIN!\nAMAZING JOB";

        if (GameTimer.Instance != null)
            GameTimer.Instance.StopTimer();
    }

    private void HideMessage()
    {
        messageText.gameObject.SetActive(false);
    }


    public void PlayAgain()
    {
        StartCoroutine(PlayAgainRoutine());
    }

    IEnumerator PlayAgainRoutine()
    {
        SoundManager.Instance.PlayButtonClick();
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextGame()
    {
        StartCoroutine(NextGameRoutine());
    }

    IEnumerator NextGameRoutine()
    {
        SoundManager.Instance.PlayButtonClick();
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }





}