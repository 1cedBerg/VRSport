using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private TMP_Text currentScoreText;
    [SerializeField] private TMP_Text highScoreText;

    public int CurrentScore { get; private set; }
    public int SessionHighScore { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        UpdateUI();
    }

    public void AddPoint(int amount = 1)
    {
        CurrentScore += amount;

        if (CurrentScore > SessionHighScore)
        {
            SessionHighScore = CurrentScore;
        }

        UpdateUI();
    }

    public void ResetCurrent()
    {
        CurrentScore = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (currentScoreText != null)
            currentScoreText.text = "Score: " + CurrentScore;

        if (highScoreText != null)
            highScoreText.text = "High: " + SessionHighScore;
    }
}
