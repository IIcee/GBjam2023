using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCredits : Button
{

    public override void Press()
    {
        base.Press();
        Debug.Log("Load Credits Scene");
    }
}
