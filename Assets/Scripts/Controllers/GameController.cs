using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public enum GameState { WelcomeScreen, DressupScreen, PowerupScreen, LevelOpen, WalkedIn, ClothesDropped,  JumpFailed, MarioFreaks, DoneFreaking, BadControls, JumpedAround, EndScreen}
     

    GameState currentGameState;
    public bool skipToLevel;
    public bool allowMovement = false;

    int failedJumps = 0;
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
           MarioController.Instance.misterMario.MarioSpinsAndJumps();
        }
        if (state == GameState.ClothesDropped)
        {
            SoundControllerScript.PlaySound("changing his clothes");
            allowMovement = true;
        }
        if (state == GameState.JumpFailed)
        { 
            allowMovement = false;
            StartCoroutine(PostFailDelay());
        }
        if (state == GameState.MarioFreaks)
        { 
           MarioController.Instance.misterMario.MarioFreaks(); 
        }
        if (state == GameState.DoneFreaking)
        {
            StartCoroutine(PostFreakDelay());

        }
        if (state == GameState.BadControls)
        {
            MarioController.Instance.FlipControls();
            allowMovement = true;
        } 
        if (state == GameState.JumpedAround)
        {            
            allowMovement = false;
            MarioController.Instance.misterMario.MarioWalksOff();
        }
        if (state == GameState.EndScreen)
        {
            UIController.Instance.LoadEndScreen();
        }
        currentGameState = state;
    }

    public GameState GetGameState()
    {
        return currentGameState;
    }


    IEnumerator PostFailDelay()
    {
        for (float t = 0.0f; t < 2f; t += Time.deltaTime * 2)
            yield return null;
        while (MarioController.Instance.misterMario.gameObject.transform.localPosition.y > 2f)
            yield return null;
        SetGameState(GameState.MarioFreaks);
    }
    IEnumerator PostFreakDelay()
    {
        for (float t = 0.0f; t < 0.5f; t += Time.deltaTime * 2)
            yield return null;
        SetGameState(GameState.BadControls);
    }
    IEnumerator PostJumpFailDelay()
    {
        for (float t = 0.0f; t < 8f; t += Time.deltaTime * 2)
            yield return null;
        SetGameState(GameState.JumpedAround);
    }

    public void FailJump()
    {
        failedJumps++;
        if (failedJumps == 8)
        {
            allowMovement = false;
            StartCoroutine(PostJumpFailDelay());
        }
    }
}
