using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance;


    public GameObject startPanel;
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

    public void ShowTitleScreen()
    {
        startPanel.gameObject.SetActive(true);
    } 
    public void UpdateTitleScreen()
    {
        textOrginal.gameObject.SetActive(false);
        titleScreenUpdated = true;

        if (buttonPressedI == true)
        {
            textL.gameObject.SetActive(true);
        }
        if (buttonPressedL == true)
        {
            textI.gameObject.SetActive(true);
        } 

    }

    public void ClearTitleScreen()
    {
        startPanel.gameObject.SetActive(false);

    }


}
