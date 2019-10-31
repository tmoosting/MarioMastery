using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public enum GameState { WelcomeScreen, DressupScreen, PowerupScreen, LevelOpen, WalkedIn, ClothesDropped,  JumpFailed, EndScreen}

    public enum WeaponType {  Sword, Axe, Arrow}

    GameState currentGameState;
    public bool skipToLevel;
    public bool allowMovement = false;

    void Awake()
    {
        Instance = this; 
    }

    private void Start()
    {
        OpenGame();
        TextController.Instance.HideTextPanel();
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
            SoundControllerScript.PlaySound("background sound");
            UIController.Instance.ShowTitleScreen();
        }
        if (state == GameState.DressupScreen)
        {
       //     TextController.Instance.ShowTextPanel(state);
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
        if (state == GameState.WalkedIn)
        {
            allowMovement = true;
        }
        if (state == GameState.ClothesDropped)
        {
            SoundControllerScript.PlaySound("changing his clothes");
        }
        currentGameState = state;
    }

    public GameState GetGameState()
    {
        return currentGameState;
    }
}
