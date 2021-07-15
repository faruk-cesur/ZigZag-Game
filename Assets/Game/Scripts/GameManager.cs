using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton Design Pattern
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }


    public PlayerController player;
    private GameState _currentGameState;
    public GameObject prepareUI;
    public GameObject gameOverUI;
    public GameObject mainGameUI;
    private int score;
    private int diamondScore;
    public Text scoreText;
    public Text goScoreText;
    public Text bestScoreText;
    public Text diamondText;


    // Using Game States Enum For Functionality
    public enum GameState
    {
        Prepare,
        MainGame,
        GameOver
    }

    // Using extra switch for game state to run one time codes.
    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        set
        {
            switch (value)
            {
                case GameState.Prepare:
                    break;
                case GameState.MainGame:
                    break;
                case GameState.GameOver:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            _currentGameState = value;
        }
    }

    // Doing things in update when game state changes
    private void Update()
    {
        switch (CurrentGameState)
        {
            case GameState.Prepare:
                if (Input.GetMouseButtonDown(0))
                {
                    CurrentGameState = GameState.MainGame;
                }
                gameOverUI.SetActive(false);
                prepareUI.SetActive(true);
                mainGameUI.SetActive(false);
                break;
            case GameState.MainGame:
                player.BallMovement();
                prepareUI.SetActive(false);
                player.DeathState();
                mainGameUI.SetActive(true);
                BestScore();
                DiamondScore();
                break;
            case GameState.GameOver:
                gameOverUI.SetActive(true);
                mainGameUI.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    // Reloads the same scene. Using with button
    public void Retry()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    
    public void UpdateScore()
    {
        score++;
        scoreText.text = goScoreText.text;
        scoreText.text = score.ToString();
        goScoreText.text = scoreText.text;
    }
    
    public void BestScore()
    {
        if (!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
        if (score > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", score);
        }

        bestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();
    }
    
    public void DiamondScore()
    {
        if (!PlayerPrefs.HasKey("DiamondScore"))
        {
            PlayerPrefs.SetInt("DiamondScore", diamondScore);
        }
        if (diamondScore <= PlayerPrefs.GetInt("DiamondScore"))
        {
            PlayerPrefs.SetInt("DiamondScore", diamondScore+PlayerPrefs.GetInt("DiamondScore"));
        }

        bestScoreText.text = PlayerPrefs.GetInt("DiamondScore").ToString();
    }
    
    public void CollectDiamond()
    {
        diamondScore++;
        diamondText.text = diamondScore.ToString();
    }

    public void DeathArea()
    {
        CurrentGameState = GameState.GameOver;
    }
}