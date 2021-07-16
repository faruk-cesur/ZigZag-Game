using System;
using System.Collections;
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

    // private void Start()
    // {
    //     PlayerPrefs.DeleteAll();
    // }


    public PlayerController player;
    private GameState _currentGameState;
    public GameObject prepareUI;
    public GameObject gameOverUI;
    public GameObject mainGameUI;
    public GameObject particleDiamond;
    private int score;
    private int diamondScore;
    public Text scoreText;
    public Text goScoreText;
    public Text bestScoreText;
    public Text prepareBestScore;
    public Text diamondText;
    public Text prepareDiamondScoreText;
    public AudioClip moveSound;
    public AudioClip deathSound;
    public AudioClip diamondSound;
    public AudioClip multipleScoreSound;
    public AudioClip startSound;
    public Material material;

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
                player.BallMovement();
                prepareBestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
                prepareDiamondScoreText.text = PlayerPrefs.GetInt("DiamondScore").ToString();
                if (player.isStarted)
                {
                    AudioSource.PlayClipAtPoint(startSound,player.transform.position);
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
    
    public void CollectDiamond()
    {
        diamondScore++;
        diamondText.text = diamondScore.ToString();
        if (!PlayerPrefs.HasKey("DiamondScore"))
        {
            PlayerPrefs.SetInt("DiamondScore", diamondScore);
        }
        if (true)
        {
            PlayerPrefs.SetInt("DiamondScore", 1+PlayerPrefs.GetInt("DiamondScore"));
        }

        prepareDiamondScoreText.text = PlayerPrefs.GetInt("DiamondScore").ToString();
    }

    public void DeathArea()
    {
        StartCoroutine(ChangingGameOver());
    }

    IEnumerator ChangingGameOver()
    {
        AudioSource.PlayClipAtPoint(deathSound,player.transform.position);
        yield return new WaitForSeconds(1f);
        CurrentGameState = GameState.GameOver;
    }
}