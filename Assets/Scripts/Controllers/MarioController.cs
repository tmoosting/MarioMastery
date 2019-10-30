using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    public static MarioController Instance;



    void Awake()
    {
        Instance = this;
    }

    public Mario misterMario;
   
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
}
