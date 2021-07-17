using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    // Singleton Design Pattern to reach GameManager everywhere.
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    // All Variables In Game Manager
    public PlayerController player;
    public Camera camera;
    public GameState currentGameState;
    public GameObject prepareUI;
    public GameObject gameOverUI;
    public GameObject mainGameUI;
    public GameObject shopUI;
    public GameObject particleDiamond;
    private int score;
    private int diamondScore;
    public Text scoreText;
    public Text goScoreText;
    public Text bestScoreText;
    public Text prepareBestScore;
    public Text diamondText;
    public Text prepareDiamondScoreText;
    public Text shopDiamondScoreText;
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

    // Doing things in update when game state changes
    private void Update()
    {
        switch (currentGameState)
        {
            case GameState.Prepare:
                player.BallMovement();
                prepareBestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
                prepareDiamondScoreText.text = PlayerPrefs.GetInt("DiamondScore").ToString();
                shopDiamondScoreText.text = PlayerPrefs.GetInt("DiamondScore").ToString();
                if (player.isStarted)
                {
                    AudioSource.PlayClipAtPoint(startSound, camera.transform.position);
                    currentGameState = GameState.MainGame;
                }

                gameOverUI.SetActive(false);
                prepareUI.SetActive(true);
                mainGameUI.SetActive(false);
                shopUI.SetActive(false);
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

    // Score Increase Method
    public void UpdateScore()
    {
        score++;
        scoreText.text = goScoreText.text;
        scoreText.text = score.ToString();
        goScoreText.text = scoreText.text;
    }

    // Keeping Best Score on HDD
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


    // Increasing numbers of collected diamonds and keep it on HDD
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
            PlayerPrefs.SetInt("DiamondScore", 1 + PlayerPrefs.GetInt("DiamondScore"));
        }

        prepareDiamondScoreText.text = PlayerPrefs.GetInt("DiamondScore").ToString();
    }


    // When we fall, DeathArea method is running with a coroutine
    public void DeathArea()
    {
        StartCoroutine(ChangingGameOver());
    }

    IEnumerator ChangingGameOver()
    {
        AudioSource.PlayClipAtPoint(deathSound, camera.transform.position);
        yield return new WaitForSeconds(1f);
        currentGameState = GameState.GameOver;
    }
}