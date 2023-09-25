using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GBTemplate;
using System;

/*
Manages scene flow and things that persist between scenes, like options/settings.
*/
public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; } //Can be accessed, but not changed.

    int currentSceneIndex;

    private GBConsoleController gb;

    [SerializeField] AudioClip levelMusic;
    [SerializeField] AudioClip victoryMusic;

    //On awake create instance that persists between scenes
    private void Awake()
    {
        //Destroys extra copies
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //runs at start, initializes gb
    private void Start()
    {
        gb = GBConsoleController.GetInstance();
    }

    //Loads next scene
    public IEnumerator LoadNextScene()
    {
        //fade
        yield return gb.Display.StartCoroutine(gb.Display.FadeToBlack(2));

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        gb.Sound.PlayMusic(levelMusic);

        //fade back
        yield return gb.Display.StartCoroutine(gb.Display.FadeFromBlack(2));
    }

    //Loads start scene
    public IEnumerator LoadStartScene()
    {
        //fade
        yield return gb.Display.StartCoroutine(gb.Display.FadeToBlack(2));

        SceneManager.LoadScene(0);

        //fade back
        yield return gb.Display.StartCoroutine(gb.Display.FadeFromBlack(2));
    }

    //Loads victory scene
    public IEnumerator LoadVictoryScene()
    {
        //fade
        yield return gb.Display.StartCoroutine(gb.Display.FadeToBlack(2));

        SceneManager.LoadScene("VictoryScreen");
        gb.Sound.PlayMusic(victoryMusic);

        //fade back
        yield return gb.Display.StartCoroutine(gb.Display.FadeFromBlack(2));
    }

    //Pause stops the time and brings up the pause screen.
    public void Pause()
    {
        Time.timeScale = 0;
        //There must be a better way of doing this, but this way this can persist between scenes
        //Pause Screens need to be under a pause parent...
        GameObject pauseParent = GameObject.FindGameObjectWithTag("Pause");
        GameObject pauseScreen = pauseParent.transform.GetChild(0).gameObject;
        pauseScreen.SetActive(true);
    }

    //Unpause removes the pause screen and sets timescale back to one.
    public void UnPause()
    {
        GameObject pauseParent = GameObject.FindGameObjectWithTag("Pause");
        GameObject pauseScreen = pauseParent.transform.GetChild(0).gameObject;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    //Resets scene
    public IEnumerator ResetScene()
    {
        Time.timeScale = 1; //make sure time is ok in case of pausing
        Debug.Log(1);
        //fade
        yield return gb.Display.StartCoroutine(gb.Display.FadeToBlack(2));
        Debug.Log(2);
        //load next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log(3);
        //fade back
        yield return gb.Display.StartCoroutine(gb.Display.FadeFromBlack(2));
        Debug.Log(4);

    }

    /*
    OPTIONS STUFF CURRENTLY UNUSED
    //Takes you to the options screen
    public void LoadOptions()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Banana0");
        StartCoroutine(OptionsFade());

    }

    //A method to return to the "current scene" from the options screen.
    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    //experimenting with fade
    public IEnumerator OptionsFade()
    {
        Debug.Log("Banana");
        yield return gb.Display.StartCoroutine(gb.Display.FadeToBlack(2));
        Debug.Log("Banana2");
        //Add Options Scene: SceneManager.LoadScene(0);
        yield return gb.Display.StartCoroutine(gb.Display.FadeFromBlack(2));
        LoadCurrentScene();
    }
    */
}
