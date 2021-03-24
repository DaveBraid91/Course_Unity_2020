using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    [SerializeField]
    private float fadeDuration, timer, displayImageDuration;
    private bool isPlayerAtExit, isPlayerCaught, hasAudioPlayed;
    [SerializeField]
    private GameObject player;

    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource exitAS, caughtAS;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            isPlayerAtExit = true;
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, exitAS);
        }
        else if(isPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, caughtAS, true);
        }
    }
    /// <summary>
    /// Shows the end game screen
    /// </summary>
    /// <param name="canvasToShow">The screen it shows, depending on how the game ended</param>
    void EndLevel(CanvasGroup canvasToShow, AudioSource audioSource, bool doRestart = false)
    {
        if(!hasAudioPlayed)
        {
            audioSource.Play();
            hasAudioPlayed = true;
        }
        timer += Time.deltaTime;
        canvasToShow.alpha = Mathf.Clamp(timer / fadeDuration, 0, 1);

        if (timer > fadeDuration + displayImageDuration)
        {
            if(doRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Application.Quit();
                Debug.Log("Hemos salido del juego.");
            }
            
        }
        
    }

    public void PlayerCaught()
    {
        isPlayerCaught = true;
    }
}
