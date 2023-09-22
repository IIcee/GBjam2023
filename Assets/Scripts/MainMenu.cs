using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GBTemplate;
using UnityEngine.UI;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

/*
Script for things in the main menu.
Currently has methods to exit the game, switch to credits view, and open the startand options scenes.
*/
public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject creditText;
    [SerializeField] GameObject menuBG;
    [SerializeField] GameObject menuButts;    //hehe

    bool isCredits;

    private GBConsoleController gb;
    [SerializeField] AudioClip titleMusic;
    [SerializeField] AudioClip selectAudio; //select audio plays when returning to menu




    //StartNew starts the first scene of the game.
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    //runs at start, initializes gb
    private void Start()
    {
        gb = GBConsoleController.GetInstance();
        gb.Sound.PlayMusic(titleMusic);
    }

    //runs every frame of the game
    private void Update()
    {   
        //Can return from credits scene with either A or B buttons
        if (isCredits && (gb.Input.ButtonAJustPressed || gb.Input.ButtonBJustPressed))
        {
            CreditToggler();
        }
    }

    //Toggles the scene between credits and main menu.
    public void CreditToggler()
    {
        if (creditText.activeInHierarchy){
            gb.Sound.PlaySound(selectAudio);
        }
        creditText.SetActive(!creditText.activeInHierarchy);
        menuBG.SetActive(!menuBG.activeInHierarchy);
        menuButts.SetActive(!menuButts.activeInHierarchy);
        StartCoroutine(CreditDelay());
    }

    IEnumerator CreditDelay(){
        yield return new WaitForSeconds(0.5f);
        isCredits = !isCredits;
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
}
