using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI enemyLeft;
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
        enemyLeft.text = "Enemy Left: ";
    }

    public void SetNumberEnemies(int currentEnemies)
    {
        enemyLeft.text = "Enemy Left: ";
        if (currentEnemies <= 0)
        {
            GameOver();
        }
        else
        {
            enemyLeft.text += currentEnemies;
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        enemyLeft.text = "0";
        gameOverScreen.SetActive(true);
    }
}