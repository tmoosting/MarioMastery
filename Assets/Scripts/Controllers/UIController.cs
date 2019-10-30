using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{ 
    public static UIController Instance;


    public GameObject canvas;
    public GameObject startPanel;
    public GameObject dressupPanel;
    public GameObject powerupPanel;
    public TextMeshProUGUI clickToStartText;
    public TextMeshProUGUI textOrginal;
    public TextMeshProUGUI textI;
    public TextMeshProUGUI textL;


    bool titleScreenUpdated = false;
    bool buttonPressedI = false;
    bool buttonPressedL = false;


    void Awake()
    {
        Instance = this;
        startPanel.SetActive(false);
        dressupPanel.SetActive(false);
        powerupPanel.SetActive(false);
        canvas.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown("i") && titleScreenUpdated == false)
        {
            buttonPressedI = true;
            UpdateTitleScreen();
        }
        else if (Input.GetKeyDown("l") && titleScreenUpdated == false)
        {
            buttonPressedL = true;
            UpdateTitleScreen();
        }
        if (titleScreenUpdated == true)
        {
            if (buttonPressedI == true)
                if (Input.GetKeyDown("l"))
                    ClearTitleScreen(); 
            if (buttonPressedL == true)
                if (Input.GetKeyDown("i"))
                    ClearTitleScreen();
        }
    }

    // TITLE SCREEN
    public void ShowTitleScreen()
    {
        startPanel.SetActive(true);
    } 
    public void UpdateTitleScreen()
    {
        textOrginal.gameObject.SetActive(false);
        titleScreenUpdated = true;

        if (buttonPressedI == true)        
            textL.gameObject.SetActive(true);        
        if (buttonPressedL == true)        
            textI.gameObject.SetActive(true); 
    }
    public void ClearTitleScreen()
    {
        startPanel.SetActive(false);
        GameController.Instance.SetGameState(GameController.GameState.DressupScreen);
    }

    // DRESSUP SCREEN
    public void ShowDressUpScreen()
    {
        dressupPanel.SetActive(true);
        DressController.Instance.InitializeDressPanel();
    }
    public void ClickDressupButton()
    {
        if (MarioController.Instance.DressupComplete())        
            ClearDressUpScreen();        
    }
    public void ClearDressUpScreen()
    {
        dressupPanel.SetActive(false);
        MarioController.Instance.SetMarioDress(DressController.Instance.GetChoiceIndexes());
        GameController.Instance.SetGameState(GameController.GameState.LevelOpen);
    }

    // POWERUP SCREEN
    public void ShowPowerUpScreen()
    {
        powerupPanel.SetActive(true); 
    }
    public void ClickPowerupButton()
    {
        if (MarioController.Instance.PowerupComplete())
            ClearPowerUpScreen();
    }
    public void ClearPowerUpScreen()
    {
        powerupPanel.SetActive(false);
        GameController.Instance.SetGameState(GameController.GameState.LevelOpen); 
    }


}
