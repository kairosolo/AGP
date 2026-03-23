using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject checkpointReachedObject;
    [SerializeField] private TextMeshProUGUI readyTimerText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] public Vector3 checkPointPosition;
    [SerializeField] private float timerCount;

    private bool isGameOver = false;
    private bool isGameStart = false;
    public bool IsGameOver => isGameOver;
    public bool IsGameStart => isGameStart;

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
        StartCoroutine(ReadyTimer());
    }

    private void Update()
    {
        if (IsGameOver || !isGameStart) return;

        timerCount += Time.deltaTime;
        timerText.text = $"Time: {timerCount:0.00}";
    }

    private IEnumerator ReadyTimer()
    {
        readyTimerText.text = "Ready?";
        yield return new WaitForSeconds(1f);
        readyTimerText.text = "3";
        yield return new WaitForSeconds(1f);
        readyTimerText.text = "2";
        yield return new WaitForSeconds(1f);
        readyTimerText.text = "1";
        yield return new WaitForSeconds(1f);
        isGameStart = true;
        readyTimerText.text = "GO!";
        yield return new WaitForSeconds(.5f);
        readyTimerText.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        isGameOver = true;
        isGameStart = false;
        timerText.gameObject.SetActive(false);
        gameOverText.text = $"You win!\n{timerCount:0.00}";
        gameOverScreen.SetActive(true);
    }

    public void SetCheckpoint(Vector3 position)
    {
        StartCoroutine(Popup());
        checkPointPosition = position + new Vector3(0, 1, 0);
        Debug.Log($"Checkpoint reached at {checkPointPosition}");
    }

    private IEnumerator Popup()
    {
        checkpointReachedObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        checkpointReachedObject.SetActive(false);
    }
}