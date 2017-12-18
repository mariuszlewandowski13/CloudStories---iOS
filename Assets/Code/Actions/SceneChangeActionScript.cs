using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SceneChangeActionScript : ActionScript, IClickable {

    private UpdateEnviroment manager;

    public override void SetActionData(string data)
    {
        base.SetActionData(data);
        if (manager == null)
        {
            manager = GameObject.Find("EnviromentPlane").GetComponent<UpdateEnviroment>();
        }
    }

    public void Clicked()
    {
        ChangeScene();
    }

    private void ChangeScene()
    {
        int number;
        if (manager != null && int.TryParse(actionData, out number))
        {
            manager.ChangeSceneNumber(number);
        }
    }
}
