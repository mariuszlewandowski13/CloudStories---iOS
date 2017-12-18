using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class SceneChangeActionScript : ActionScript {

    private void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(ChangeScene);
    }

    public override void SetActionData(string data)
    {
        base.SetActionData(data);

    }

    private void ChangeScene()
    {

    }
}
