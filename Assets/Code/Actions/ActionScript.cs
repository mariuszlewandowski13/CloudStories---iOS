using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScript : ClickableButton {

    public string actionData;

    public virtual void SetActionData(string data)
    {
        actionData = data;
    }
}
