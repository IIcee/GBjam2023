using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using GBTemplate;

/*
Custom button class.
Holds info for:
    -Selected Sprite to switch to
    -Selection bool
    -next button to select
    -prev button to select
Has methods for:
    -navigation between buttons
    -being selected
    -being pressed that prints press - overridden for each child script
*/
public class Button : MonoBehaviour
{
    [SerializeField] Sprite selectedSprite;
    Sprite buttonSprite;

    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject prevButton;
    

    public bool isSelected;

    protected GBConsoleController gb;

    // Start is called before the first frame update
    protected void Start()
    {
        gb = GBConsoleController.GetInstance();
        buttonSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame. Calls navigation if button is selected.
    protected void LateUpdate()
    {
        if (isSelected)
        {
            Navigation();
        }
    }

    //Called when selected. Handles keyboard inputs for navigation.
    //Needs debugging it is broken for some reason.
    protected virtual void Navigation()
    {
        if (gb.Input.ButtonAJustPressed)
        {
            Debug.Log($"A {prevButton.GetComponent<SpriteRenderer>().sprite}");
            Press();
        }
        if (gb.Input.UpJustPressed)
        {
            Debug.Log($"UP {prevButton.GetComponent<SpriteRenderer>().sprite}");
            Unselect();
            prevButton.GetComponent<Button>().Select();
        }
        if (gb.Input.DownJustPressed)
        {
            Debug.Log($"Down {prevButton.GetComponent<SpriteRenderer>().sprite}");
            Unselect();
            nextButton.GetComponent<Button>().Select();
        }
    }

    //Sets button as selected and switches sprite to selected one.
    public void Select()
    {
        isSelected = true;
        //gameObject.SetActive(false);
        //selectedSprite.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().sprite = selectedSprite;
    }

    //Sets button as not selected and switches sprites back.
    protected void Unselect()
    {
        isSelected = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = buttonSprite;

    }

    //Presses the button.
    protected virtual void Press()
    {
        Debug.Log("Button Pressed!");
    }
}
