using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    void Awake()
    {
        Instance = this; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
