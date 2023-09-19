using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCredits : Button
{

    protected override void Press()
    {
        base.Press();
        Debug.Log("Load Credits Scene");
    }
}
