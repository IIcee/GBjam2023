using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    [SerializeField] int currentLevel;

    private void Awake()
    {
        currentLevel = 1;
    }

    public void VictimSaved()
    {
        currentLevel += 1;
        ChangeLevel();
    }

    private void ChangeLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + currentLevel);
        Debug.Log("Level: " + currentLevel);
    }
}
