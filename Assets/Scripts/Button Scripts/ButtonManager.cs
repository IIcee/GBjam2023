using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBTemplate;
using UnityEngine.UI;
using System;

/*
Custom class to manage all buttons.
Holds info for:
    -Array of custom Buttom class
    -Int that keeps track of which sprite is selected
Has methods for:
    -navigation between buttons
    -Action when selected
*/

public class ButtonManager : MonoBehaviour
{
    protected GBConsoleController gb;
    [SerializeField] Button[] buttons;
    [SerializeField] int buttonIndex = 0;


    // Start is called before the first frame update
    //initializes gbcontroller and selects start
    void Start()
    {
        gb = GBConsoleController.GetInstance();
        buttons[buttonIndex].Select();
    }

    // Update is called once per frame. Calls navigation.
    void Update()
    {
        Navigate();
    }

    //Manages button presses and calls navigation support methods.
    protected virtual void Navigate()
    {
        if (gb.Input.ButtonAJustPressed)
        {
            Debug.Log($"A.");
            buttons[buttonIndex].Press();
        }
        if (gb.Input.UpJustPressed)
        {
            Debug.Log($"UP");
            SelectPrev();
        }
        if (gb.Input.DownJustPressed)
        {
            Debug.Log($"Down");
            SelectNext();
        }
    }

    //Selects the next button and adjusts button index
    private void SelectNext()
    {
        buttons[buttonIndex].Unselect();
        buttonIndex = (buttonIndex+1)%buttons.Length;
        Debug.Log(buttonIndex);
        buttons[buttonIndex].Select();
    }

    //Selects the previous button and adjusts button index
    private void SelectPrev()
    {
        buttons[buttonIndex].Unselect();
        buttonIndex = (buttons.Length+buttonIndex-1)%buttons.Length;
        Debug.Log(buttonIndex);
        buttons[buttonIndex].Select();
    }
}
