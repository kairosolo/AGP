using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private float gameDuretion;
    private float currentGameDuretion;
    [SerializeField] private TextMeshProUGUI timerText;
    private bool isGameOver = false;
    public bool IsGameOver => isGameOver;

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
        currentGameDuretion = gameDuretion;
        timerText.text = "Time: " + Math.Round(currentGameDuretion, 2);
    }

    private void Update()
    {
        currentGameDuretion -= Time.deltaTime;
        timerText.text = "Time: " + Math.Round(currentGameDuretion, 2);

        if (currentGameDuretion <= 0)
        {
            isGameOver = true;
            timerText.text = "0";
            gameOverScreen.SetActive(true);
        }
    }
}