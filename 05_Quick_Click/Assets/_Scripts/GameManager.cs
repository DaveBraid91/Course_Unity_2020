using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        inGame,
        loading,
        paused,
        gameOver
    }

    public GameState gameState;
    public GameObject titleScreen;
    public List<GameObject> targetPrefabs;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public List<GameObject> lives;

    

    [SerializeField]
    private float spawnRate = 1;
    private int _score, numberOfLives = 3;
    private int Score
    {
        set
        {
            _score = Mathf.Clamp(value, 0, 99999999);
        }
        get
        {
            return _score;
        }
    }

    private void Start()
    {
        ShowMaxScore();
    }
    /// <summary>
    /// Spawns a target every spawnRate seconds
    /// </summary>
    /// <returns>Waits spawnRate seconds</returns>
    IEnumerator SpawnTarget()
    {
        while(gameState == GameState.inGame)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);
            Instantiate(targetPrefabs[index]);
        }
    }

    /// <summary>
    /// Sets a new max score, if there has been one
    /// </summary>
    private void SetMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt("MAX_SCORE");
        if (Score > maxScore)
        {
            PlayerPrefs.SetInt("MAX_SCORE", Score);
        }
    }

    /// <summary>
    /// Shows the max score in the UI
    /// </summary>
    public void ShowMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt("MAX_SCORE", 0);
        scoreText.text = "Max Score: " + maxScore;
    }

    /// <summary>
    /// Starts the game, changing the game state and setting the score to zero
    /// </summary>
    /// <param name="difficulty">Integer number that determines the spawn rate 
    /// (the higher the difficulty, the shorter is the spawn rate)</param>
    public void StartGame(int difficulty)
    {
        gameState = GameState.inGame;
        titleScreen.gameObject.SetActive(false);
        spawnRate /= difficulty;
        //numberOfLives -= difficulty;

        for(int i = 0; i < numberOfLives; i++)
        {
            lives[i].SetActive(true);
        }

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
    }

    /// <summary>
    /// Updates the score and shows it in the UI
    /// </summary>
    /// <param name="scoreToAdd">Points to add to the actual score</param>
    public void UpdateScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        scoreText.text = "Score: " + Score;
    }

    public void SubstractLives()
    {
        numberOfLives--;
        if(numberOfLives >= 0)
        {
            Image heartImage = lives[numberOfLives].GetComponent<Image>();
            Color tempColor = heartImage.color;
            tempColor.a = 0.3f;
            heartImage.color = tempColor;
        }
    }

    /// <summary>
    /// Finishes the game, changing the game state and showing the Game Over UI
    /// </summary>
    public void GameOver()
    {
        SubstractLives();
        if(numberOfLives <= 0)
        {
            SetMaxScore();
            gameState = GameState.gameOver;
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Restarts the game
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
