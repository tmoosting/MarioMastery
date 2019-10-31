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
     
    void Awake()
    {
        Instance = this; 
    }




    public void ShowTextPanel()
    {

    }

    public void HideTextPanel()
    {

    }

}
