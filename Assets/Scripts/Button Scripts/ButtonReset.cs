using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReset : Button
{
    //resets the scene
    public override void Press()
    {
        base.Press();
        StartCoroutine(mainManager.ResetScene());
    }
}
