using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public bool OpenWelcomeUI;

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
        if (OpenWelcomeUI == true)
          UIController.Instance.ShowTitleScreen();
    }

    void StartGame()
    {

    }
}
