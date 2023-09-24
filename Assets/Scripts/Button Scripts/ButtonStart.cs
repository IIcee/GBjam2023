using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonStart : Button
{   
    //this function loads the next scene
    public override void Press()
    {
        base.Press();
        StartCoroutine(MainManager.Instance.LoadNextScene());
    }

    //Example fade enumerator with testing scripts
    public IEnumerator StartFade()
    {
        Debug.Log("Banana");
        yield return gb.Display.StartCoroutine(gb.Display.FadeToBlack(2));
        Debug.Log("Banana2");
        //transition to next scene here actually and the fading from black would probs be done there.
        yield return gb.Display.StartCoroutine(gb.Display.FadeFromBlack(2));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
