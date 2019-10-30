using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public TextMeshProUGUI txt;


    void Awake()
    {
        Instance = this;
    }

   

    public void ShowTitleScreen()
    {

    }

    public void ClickTitleScreen()
    {

    }
}
