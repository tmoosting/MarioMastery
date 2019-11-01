using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioAI : MonoBehaviour
{

    public enum Trigger { EnterPlatform, ExitPlatform, FailJumpMidAir, FallJumpLand, JumpEverShorter, WalkOffscreen }
    public enum MarioAction { None,  WalkBackOffPlatform,FreakOut, InvertControls, LimitJumps, WalkIntoSunshine } 

    Mario mario;

    // Triggers
    bool onPlatform;
    int platformEnterCount = 0; 
    int platformExitCount = 0; 
    int jumpFailCount = 0;
    int jumpShortCount = 0;

    bool waitTrigger = false;
    bool countFrames = false;

    Dictionary<MarioAction, System.Action> marioActionMapping = new Dictionary<MarioAction, System.Action>();
    int frame = 0;
    int sequenceCount = 0;

    void Update()
    {
     
            frame++;
       
    }

    void Start()
    {
        mario = MarioController.Instance.mario; 
        marioActionMapping.Add(MarioAction.WalkBackOffPlatform, MarioWalkBackOffPlatform);
        marioActionMapping.Add(MarioAction.FreakOut, MarioFreakOut);
        marioActionMapping.Add(MarioAction.InvertControls, MarioInverted);
        marioActionMapping.Add(MarioAction.LimitJumps, MarioJumpHandicapped);
        marioActionMapping.Add(MarioAction.WalkIntoSunshine, MarioWalkSunshine);
       
    }

    public void CallTrigger(Trigger trigger)
    {
        if (trigger == Trigger.EnterPlatform)
            TriggerEnterPlatform();
        if (trigger == Trigger.ExitPlatform)
            TriggerExitPlatform();
        if (trigger == Trigger.FailJumpMidAir)
            TriggerMidAirFail();
        if (trigger == Trigger.FallJumpLand)
            TriggerFailJumpLand();
        if (trigger == Trigger.JumpEverShorter)
            TriggerJumpShort();  
        //if (trigger == Trigger.WalkOffscreen)
        //    TriggerJumpShort();
    }
    public void CallAction(MarioAction action)
    {
        if (marioActionMapping.ContainsKey(action))
            marioActionMapping[action]();
        else
            Debug.Log("Calling action that is not in dictionary" + action.ToString());
    }



    public void StartSequence()
    {
        StartCoroutine(Sequence());    
    }
    IEnumerator Sequence()
    { 
        if (sequenceCount == 0)
        {
            LockMario();
            ResetFrameCount(); 
            yield return new WaitUntil(() => frame >= 50); 
            TextController.Instance.CallText(TextController.TextAction.SetDitchClothesText);
            sequenceCount++;
        }
        if (sequenceCount == 1)
        {
            ResetFrameCount(); 
            yield return new WaitUntil(() => frame >= 100);
           TextController.Instance.ClearText(); 
            mario.MarioSpinJumps();
            sequenceCount++;
        }
        if (sequenceCount == 2)
        {
            UnlockMario(); 
            yield return new WaitUntil(() => platformEnterCount==2);
            ResetFrameCount();
            yield return new WaitUntil(() => frame >= 60);
            TextController.Instance.CallText(TextController.TextAction.SetLetsGoText);
            sequenceCount++;
        }
        if (sequenceCount == 3)
        { 
            yield return new WaitUntil(() => jumpFailCount == 1);
            TextController.Instance.CallText(TextController.TextAction.SetExcuseText);
            sequenceCount++;
        }
        if (sequenceCount == 4)
        { 
            yield return new WaitUntil(() => jumpFailCount == 2);
            LockMario();
            TextController.Instance.CallText(TextController.TextAction.SetThoughtText); 
            sequenceCount++;
        }
        if (sequenceCount == 5)
        {
            ResetFrameCount();
            yield return new WaitUntil(() => frame >= 200);
            TextController.Instance.ClearText(); 
            CallAction(MarioAction.FreakOut);
            sequenceCount++;
        }
        if (sequenceCount == 6)
        {
            UnlockMario(); 
            ResetPlatformCount();
            yield return new WaitUntil(() => platformEnterCount == 1);
            LockMario(); 
            sequenceCount++;
        }
        if (sequenceCount == 7)
        {
            ResetFrameCount();
            yield return new WaitUntil(() => frame >= 200);
            CallAction(MarioAction.WalkBackOffPlatform);            
            sequenceCount++;
        } 
        if (sequenceCount == 8)
        {
            ResetFrameCount();
            yield return new WaitUntil(() => frame >= 50);
            UnlockMario();
            CallAction(MarioAction.InvertControls); 
            sequenceCount++;
        }  
        if (sequenceCount == 9)
        {
            ResetFrameCount();
            yield return new WaitUntil(() => frame >= 350);
            CallAction(MarioAction.LimitJumps); 
            sequenceCount++;
        }
        if (sequenceCount == 10)
        {
            yield return new WaitUntil(() => (jumpShortCount >= 11 && mario.gameObject.transform.localPosition.x < 2.2f) );
            LockMario();
            sequenceCount++;
        }
        if (sequenceCount == 11)
        {
            ResetFrameCount();
            yield return new WaitUntil(() => frame >= 250);           
            CallAction(MarioAction.WalkIntoSunshine);
            sequenceCount++;
        }
        if (sequenceCount == 12)
        {
            yield return new WaitUntil(() => mario.gameObject.transform.localPosition.x > 4.5f);
            sequenceCount++;
        }
        if (sequenceCount == 13)
        {
            ResetFrameCount();
            yield return new WaitUntil(() => frame >= 150);
            GameController.Instance.SetGameState(GameController.GameState.EndScreen);
        }
        //if (sequenceCount == 7)
        //{

        //    ResetFrameCount();
        //    ResetPlatformCount();
        //    yield return new WaitUntil(() => platformEnterCount == 1);
        //    CallAction(MarioAction.WalkBackOffPlatform);
        //    sequenceCount++;
        //}
    }
    void ResetFrameCount()
    {
        frame = 0;
    } 
    void ResetPlatformCount()
    {
        platformEnterCount = 0;
    }
   

    //public void PauseMarioForSeconds(float seconds)
    //{
    //    if (seconds == 0)
    //        seconds = 99999999f;
    //    //TODO
    //}

    public void LockMario()
    {
     //   Debug.Log("lock");
        GameController.Instance.allowMovement = false;
    }  
    public void UnlockMario()
    {
     //   Debug.Log("UNLOCK");
        GameController.Instance.allowMovement = true;

    }

   


    void TriggerEnterPlatform()
    {
        onPlatform = true;
        platformEnterCount++;

        //if (platformEnterCount == 1)
        //    TextController.Instance.SetImageTextFirstPlatformTrigger();
        //if (platformEnterCount == 1)
        //{
        //    // walk off platform action
        //} 
    }
    void TriggerExitPlatform()
    {
        onPlatform = false;
        platformExitCount++;
    }
    void TriggerMidAirFail()
    {

    }
    void TriggerFailJumpLand()
    { 
        jumpFailCount++; 
    }
    void TriggerJumpShort()
    {
        jumpShortCount++; 
    }


    // -------- ACTIONS



    void MarioFreakOut()
    {
        mario.MarioFreaks();
    }
    void MarioWalkBackOffPlatform()
    { 
      StartCoroutine(  MoveMarioRight(1.5f, 1f));
    }
       void MarioInverted()
    {
        MarioController.Instance.FlipControls();
    }
    void MarioJumpHandicapped()
    {
        MarioController.Instance.LimitJumps();
    }  
    void MarioWalkSunshine()
    {
        TextController.Instance.CallCustomTextForSeconds("ciaorivederci", 12f);
        UIController.Instance.rightWall.SetActive(false);
        StartCoroutine(MoveMarioRight(10f, 0.7f));
    }

    // ----------- COROUTINES
    //IEnumerator MarioWait(  float seconds)
    //{ 
    //    LockMario();
    //    for (float t = 0.0f; t < seconds; t += Time.deltaTime)        
    //        yield return null;        
    //    UnlockMario();  
    //}
    IEnumerator MoveMarioRight(float distance, float speed)
    {
        for (float t = 0.0f; t < distance; t += Time.deltaTime)
        {
            mario.gameObject.transform.position += Vector3.right * speed * Time.deltaTime; 
            yield return null;  
        } 
    }



}
