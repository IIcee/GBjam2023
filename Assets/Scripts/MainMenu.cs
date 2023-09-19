using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GBTemplate;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

/*
Script to attach to the canvas of the main menu.
Currently has methods to exit the game, switch to credits view, and open the startand options scenes.
*/
public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject creditText;
    [SerializeField] GameObject menuButtons;
    [SerializeField] Button startButton;
    private GBConsoleController gb;
    [SerializeField] private MainManager mainManager;
    [SerializeField] AudioClip backgroundMusic;



    //StartNew starts the first scene of the game.
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    //runs at start, initializes gb
    private void Start()
    {
        gb = GBConsoleController.GetInstance();
        mainManager = MainManager.Instance;
        gb.Sound.PlayMusic(backgroundMusic);
    }

    //runs every frame of the game
    private void Update()
    {
        //if credits are open and user pressed B, return to main menu
        if (gb.Input.ButtonB && creditText.activeInHierarchy)
        {
            ToggleCredits();
        }
    }

    //Exit quits the game. If statement is for quitting editor vs game.
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    //Toggles the credits vs menu buttons.
    public void ToggleCredits()
    {
        menuButtons.SetActive(!menuButtons.activeInHierarchy);
        creditText.SetActive(!creditText.activeInHierarchy);
        if (menuButtons.activeInHierarchy)  //selects the start button for keyboard menu navigation
        {
         startButton.Select();   
        }
    }

    //Transitions to the options screen.
    //Doesn't currently do anything, because there is no options screen yet.
    public void OptionsScreen()
    {
        mainManager.LoadOptions();
    }

}
