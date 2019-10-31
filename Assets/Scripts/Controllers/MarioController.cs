using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    public static MarioController Instance;

    public Mario misterMario;

    int failCount = 0;

    void Awake()
    {
        Instance = this;
    }


    public float PreSpinPauseTime = 0f;
    public float FreakoutBaseSpeed = 2f;


    public void SetMarioDress(List<int> indexList)
    {  
        misterMario.chosenHeadType = (Mario.HeadType)indexList[0]-1;
        misterMario.chosenBodyType = (Mario.BodyType)indexList[1]-1;
        misterMario.chosenHandsType = (Mario.HandsType)indexList[2]-1;
        misterMario.chosenFeetType = (Mario.FeetType)indexList[3]-1;  
    }
   
    public void FlipControls()
    {
        misterMario.gameObject.GetComponent<MarioCharacter>().controlsInverted = true;
    }
    public bool DressupComplete()
    {
        return true;
    }
    public bool PowerupComplete()
    {
        return true;
    }


    public void MoveMarioIntoLevel()
    {
        // TODO: mario moves in from the side of the screen.  
       StartCoroutine(MoveMarioRight());


    }
    void MarioHasMovedIntoLevel()
    {
        LevelController.Instance.leftWall.SetActive(true);
        GameController.Instance.SetGameState(GameController.GameState.WalkedIn); 
    }
    IEnumerator MoveMarioRight( )
    {
        Transform mt = misterMario.gameObject.transform;
        Vector2 currentPos = mt.localPosition; 

        for (float t = 0.0f; t < 2.5f; t += Time.deltaTime )
        {
            mt.position += Vector3.right *  0.5f * Time.deltaTime;
            yield return null;
    
        }
        MarioHasMovedIntoLevel();
    }


    
    public void MarioHasFailedJump()
    {
      //  Debug.Log("failed pre");
        StartCoroutine(FailDelay());
        failCount++;
        if (failCount == 1)
            StartCoroutine(Text3OnLand());
        if (failCount > 1)
        {
            StartCoroutine(Text4OnLand());
            StartCoroutine(WaitToHitGround());
        }
    }
    IEnumerator Text3OnLand()
    {
        while (misterMario.GetComponent<MarioCharacter>().IsGrounded() == false)
            yield return null;
        misterMario.SetText3();
    }
    IEnumerator Text4OnLand()
    {
        while (misterMario.GetComponent<MarioCharacter>().IsGrounded() == false)
            yield return null;
        misterMario.SetText4();
    }
    IEnumerator FailDelay()
    {
        for (float t = 0.0f; t < 2f; t += Time.deltaTime * 2)
        {
            yield return null;       
        }        
        misterMario.failTriggered = false;
       // Debug.Log("failed post");
    }
    IEnumerator WaitToHitGround()
    {
        while (misterMario.gameObject.GetComponent<MarioCharacter>().IsGrounded() == false)
            yield return null;

        GameController.Instance.SetGameState(GameController.GameState.JumpFailed);

    }
}
