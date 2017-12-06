using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class ReturnToScene : MonoBehaviour {

    public string SceneName;

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(ReturnTo);
    }

    void ReturnTo()
    {
        
        if (SceneName != "")
        {
            SceneManager.LoadScene(SceneName);
        }
       
    }
}
