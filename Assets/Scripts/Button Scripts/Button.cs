using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using GBTemplate;

/*
Custom button class.
Holds info for:
    -Main sprite
    -Selected sprite to switch to
    -Button function as delegate
Has methods for:
    -being pressed
    -switching sprites
*/
public class Button : MonoBehaviour
{
    [SerializeField] Sprite selectedSprite;
    [SerializeField] Sprite buttonSprite;
    
    protected GBConsoleController gb;
    protected MainManager mainManager;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        gb = GBConsoleController.GetInstance();
        mainManager = MainManager.Instance;
    }

    // Update is called once per frame. Calls navigation if button is selected.
    protected void Update()
    {

    }

    //Switches sprite to selected one.
    public void Select()
    {
        GetComponent<SpriteRenderer>().sprite = selectedSprite;
    }

    //Switches sprites back.
    public void Unselect()
    {
        GetComponent<SpriteRenderer>().sprite = buttonSprite;
    }

    //Presses the button.
    public virtual void Press()
    {
        Debug.Log("Button Pressed!");
    }
}
