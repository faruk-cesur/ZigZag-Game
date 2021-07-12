using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton Design Pattern
    public static GameManager gameManager;

    private void Awake()
    {
        gameManager = this;
    }


    public PlayerController player;
    private GameState _currentGameState;


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
                break;
            case GameState.MainGame:
                break;
            case GameState.GameOver:
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
}