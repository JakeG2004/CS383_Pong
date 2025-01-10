using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private KeyCode _menu = KeyCode.Escape;

    [SerializeField] private int _maxScore = 10;

    private int leftScore = 0;
    private int rightScore = 0;

    [SerializeField] private Text leftScoreText;
    [SerializeField] private Text rightScoreText;
    [SerializeField] private Text winText;

    public UnityEvent pauseGame;
    public UnityEvent unPauseGame;

    private bool _isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(_menu))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if(!_isPaused)
        {
            pauseGame.Invoke();
        }

        else
        {
            unPauseGame.Invoke();
        }

        _isPaused = !_isPaused;
    }

    public void OnRightScore()
    {
        rightScore++;

        if(rightScore >= _maxScore)
        {
            Debug.Log("Right Win");
            pauseGame.Invoke();
            _isPaused = true;
            winText.text = "Right Wins!";
        }

        rightScoreText.text = "Score: " + rightScore.ToString();
    }

    public void OnLeftScore()
    {
        leftScore++;

        if(leftScore >= _maxScore)
        {
            Debug.Log("Left Win");
            pauseGame.Invoke();
            _isPaused = true;
            winText.text = "Left wins!";
        }

        leftScoreText.text = "Score: " + leftScore.ToString();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetGame()
    {
        // Reset scores
        leftScore = 0;
        rightScore = 0;

        // Reset texts
        leftScoreText.text = "Score: " + leftScore.ToString();
        rightScoreText.text = "Score: " + rightScore.ToString();
        winText.text = "";
    }
}
