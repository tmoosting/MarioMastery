using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    public GameObject levelHolder;
    public GameObject leftWall;

    void Awake()
    {
        Instance = this;
        levelHolder.SetActive(false);
    }



    public void LoadLevel()
    {
        levelHolder.SetActive(true);
        DressController.Instance.LoadDressToCharacter();
        //Debug.Log(" head : " + MarioController.Instance.misterMario.chosenHeadType);
        //Debug.Log(" body : " + MarioController.Instance.misterMario.chosenBodyType);
        //Debug.Log(" hand : " + MarioController.Instance.misterMario.chosenHandsType);
        //Debug.Log(" feet: " + MarioController.Instance.misterMario.chosenFeetType);

        MarioEntersLevel();

    }


    public void MarioEntersLevel()
    {
      
        MarioController.Instance.MoveMarioIntoLevel();
    }
}
