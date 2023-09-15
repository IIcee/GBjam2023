using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    //StartNew starts the first scene of the game.
    public void StartNew()
    {
        SceneManager.LoadScene(1);
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
    }

    //Transitions to the options screen.
    //Doesn't currently do anything, because there is no options screen yet.
    public void OptionsScreen(){
        //SceneManager.LoadScene(ENTER OPTIONS SCENE NUMBER HERE);
    }

}
