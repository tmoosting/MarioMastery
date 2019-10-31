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
    public GameObject marioPanel;
    public TextMeshProUGUI clickToStartText;
    public TextMeshProUGUI textOrginal;
    public TextMeshProUGUI textI;
    public TextMeshProUGUI textL;

    public GameObject standingPlatform;
    public GameObject marioFrame;
    public GameObject selectionHolder;
    public GameObject proceedButton;

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
        standingPlatform.SetActive(true);
        marioFrame.SetActive(false);
        selectionHolder.SetActive(false);
        proceedButton.SetActive(false);
        marioPanel.SetActive(false);
        proceedButton.SetActive(false);
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
        marioPanel.SetActive(true);
        dressupPanel.SetActive(true);
        MaterializeDressUpScreen();
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
        marioPanel.SetActive(false);

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
        GameController.Instance.SetGameState(GameController.GameState.PowerupScreen); 
    }



    void MaterializeDressUpScreen()
    {
        marioFrame.SetActive(true);
        StartCoroutine(FadeTo(1.0f, 0.1f));
       
    }
    IEnumerator FadeTo(float aValue, float aTime)
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / (aTime / 4 ))
        {
      
            yield return null;
        }

        float alpha = marioFrame.GetComponent<Image>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            marioFrame.GetComponent<Image>().color = newColor;
            yield return null;
        }
        selectionHolder.SetActive(true);
        
    }

}
