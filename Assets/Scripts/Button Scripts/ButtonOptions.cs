using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOptions : Button
{   
    //WILL get to options screen, currently it cycles to next pallette
    public override void Press()
    {
        base.Press();
        gb.Display.PaletteCycleNext();
    }
}
