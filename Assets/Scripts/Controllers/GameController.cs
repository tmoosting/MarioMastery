using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public enum GameState { WelcomeScreen, DressupScreen, PowerupScreen, LevelOpen, JumpFailed, EndScreen}

    GameState currentGameState;
    public bool skipToLevel;

    void Awake()
    {
        Instance = this; 
    }

    private void Start()
    {
        OpenGame();
    }

   void OpenGame()
    {
        if (skipToLevel == true)
            SetGameState(GameState.LevelOpen);
        else
            SetGameState(GameState.WelcomeScreen); 
    } 

    public void SetGameState (GameState state)
    {
        if (state == GameState.WelcomeScreen)
        {
            UIController.Instance.ShowTitleScreen(); 
        }
        if (state == GameState.DressupScreen)
        {
            UIController.Instance.ShowDressUpScreen();
        }
        if (state == GameState.PowerupScreen)
        {
            UIController.Instance.ShowPowerUpScreen();
        }
        if (state == GameState.LevelOpen)
        {
            LevelController.Instance.LoadLevel();
        }

        currentGameState = state;
    }

    public GameState GetGameState()
    {
        return currentGameState;
    }
}
