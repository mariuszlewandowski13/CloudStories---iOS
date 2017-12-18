using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class URLActionScript : ActionScript, IClickable {


    public override void SetActionData(string data)
    {
        base.SetActionData(data);
    }

    public void Clicked()
    {
        OpenBrowser();
    }

    private void OpenBrowser()
    {
        Application.OpenURL(actionData);
    }
}
