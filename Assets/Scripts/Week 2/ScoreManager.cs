using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    [SerializeField] private int score;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = "Score: 0";
    }

    public void IncreaseScore(int addition)
    {
        score += addition;
        finalScoreText.text = "Final Score:\n" + score;
        scoreText.text = "Score: " + score;
    }
}