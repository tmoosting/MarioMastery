using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    public static MarioController Instance;

    public Mario misterMario;

    void Awake()
    {
        Instance = this;
    }

    
   
    public void SetMarioDress(List<int> indexList)
    {  
        misterMario.chosenHeadType = (Mario.HeadType)indexList[0]-1;
        misterMario.chosenBodyType = (Mario.BodyType)indexList[1]-1;
        misterMario.chosenHandsType = (Mario.HandsType)indexList[2]-1;
        misterMario.chosenFeetType = (Mario.FeetType)indexList[3]-1;  
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
  //       misterMario.MarioSpinsAndJumps();
    }
    IEnumerator MoveMarioRight( )
    {
        Transform mt = misterMario.gameObject.transform;
        Vector2 currentPos = mt.localPosition;


        for (float t = 0.0f; t < 1.5f; t += Time.deltaTime )
        {
            mt.position += Vector3.right * 1f * Time.deltaTime;
            yield return null;
    
        }
        MarioHasMovedIntoLevel();
    }
}
