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
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

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
}
