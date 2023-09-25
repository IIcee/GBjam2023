using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : Button
{
    public override void Press()
    {
        base.Press();
        StartCoroutine(mainManager.LoadStartScene());
    }
}
