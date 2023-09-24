using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ButtonExit : Button
{
        //Exits the game/editor
        public override void Press()
        {
                base.Press();
                StartCoroutine(ExitFade());
        }

        IEnumerator ExitFade()
        {
                Time.timeScale = 1; //make sure time is ok in case of pausing
                                    //fade
                yield return gb.Display.StartCoroutine(gb.Display.FadeToBlack(2));
#if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
#else
                Application.Quit();
#endif

        }
}
