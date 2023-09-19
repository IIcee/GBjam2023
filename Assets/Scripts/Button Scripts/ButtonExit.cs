using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ButtonExit : Button
{   
    //Exits the game/editor
    protected override void Press()
    {
        base.Press();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    protected override void Navigation(){
        base.Navigation();
    }
}
