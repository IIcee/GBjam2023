using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCredits : Button
{
    //Calls MainMenu script to toggle credits view.
    [SerializeField] GameObject menuManager;
    MainMenu mainMenuScript;

    protected override void Start()
    {
        base.Start();
        mainMenuScript = menuManager.GetComponent<MainMenu>();
    }

    public override void Press()
    {
        base.Press();
        mainMenuScript.CreditToggler();
    }
}
