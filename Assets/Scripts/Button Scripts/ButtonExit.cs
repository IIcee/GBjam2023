using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ButtonExit : Button
{
        [SerializeField] AudioClip pressAudio;

        //Exits the game/editor
        public override void Press()
        {
                base.Press();
                StartCoroutine(ExitSoundDelay());
        }

        IEnumerator ExitSoundDelay()
        {
                yield return new WaitForSeconds(pressAudio.length);
#if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif

        }
}
