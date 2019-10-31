using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextController : MonoBehaviour
{
    public static TextController Instance;


    public GameObject textPanel;
    public TextMeshProUGUI actualText;


    public string textIntro = "Choose an outfit for Mario";





    void Awake()
    {
        Instance = this; 
    }




    public void ShowTextPanel(GameController.GameState state)
    {
        textPanel.SetActive(true);
        if (state == GameController.GameState.DressupScreen)
            actualText.text = textIntro;
    }

    public void HideTextPanel()
    {
        textPanel.SetActive(false);

    }

}
