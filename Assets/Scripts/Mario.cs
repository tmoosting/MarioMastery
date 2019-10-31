﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
   
    public enum HeadType { Head1, Head2, Head3, Head4  }
    public enum BodyType { Body1, Body2, Body3, Body4  }
    public enum HandsType { Hands1, Hands2, Hands3, Hands4  }
    public enum FeetType { Feet1, Feet2, Feet3, Feet4  }


    public HeadType chosenHeadType;
    public BodyType chosenBodyType;
    public HandsType chosenHandsType;
    public FeetType chosenFeetType;


}