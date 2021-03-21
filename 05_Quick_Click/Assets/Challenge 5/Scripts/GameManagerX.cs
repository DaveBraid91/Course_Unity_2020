using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerX : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    //public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public GameObject gameOverPanel;
    public Slider timeSlider;
    //public Button restartButton; 

    public List<GameObject> targetPrefabs;

    private int _score;
    private int Score
    {
        set
        {
            _score = Mathf.Clamp(value, 0, 9999);
        }
        get
        {
            return _score;
        }
    }
    private float spawnRate = 1.5f, timer = 0, timeSpan = 0.5f, timeLimit = 60;
    public bool isGameActive;

    private float spaceBetweenSquares = 2.5f; 
    private float minValueX = -3.75f; //  x value of the center of the left-most square
    private float minValueY = -3.75f; //  y value of the center of the bottom-most square

    private void Start()
    {
        ShowMaxScore();
    }

    // Start the game, remove title screen, reset score, and adjust spawnRate based on difficulty button clicked
    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        timeSlider.gameObject.SetActive(true);
        StartCoroutine(SpawnTarget());
        StartCoroutine(Timer());
        Score = 0;
        timer = 0;
        UpdateScore(0);
        titleScreen.SetActive(false);
    }

    IEnumerator Timer()
    {
        while(timer < timeLimit && isGameActive)
        {
            timeSlider.value = timer / timeLimit; //Mathf.Lerp(timeSlider.value, timer / timeLimit, timeSpan * 2);
            yield return new WaitForSeconds(timeSpan);
            timer += timeSpan;
        }
        
        if(isGameActive)
        {
            GameOver();
        }
    }

    // While game is active spawn a random target
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);

            if (isGameActive)
            {
                Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
            }
            
        }
    }

    // Generate a random spawn position based on a random index from 0 to 3
    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);
        float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        return spawnPosition;

    }

    // Generates random square index from 0 to 3, which determines which square the target will appear in
    int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }

    // Update score with value from target clicked
    public void UpdateScore(int scoreToAdd)
    {
        Score += scoreToAdd;
        scoreText.text = "Score: " + Score;
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        SetMaxScore();
        gameOverPanel.gameObject.SetActive(true);
        isGameActive = false;
    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SetMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt("MAX_SCORE_2");
        if (Score > maxScore)
        {
            PlayerPrefs.SetInt("MAX_SCORE_2", Score);
        }
    }

    public void ShowMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt("MAX_SCORE_2", 0);
        scoreText.text = "Max Score: " + maxScore;
    }
}
