using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class URLActionScript : ActionScript {

    private void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OpenBrowser);
    }

    public override void SetActionData(string data)
    {
        base.SetActionData(data);
    }

    private void OpenBrowser()
    {

    }
}
