using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class ReturnToScene : MonoBehaviour {

    public ProjectsPanelManager manager;

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(ReturnTo);
    }

    void ReturnTo()
    {
        
        if (manager != null)
        {
            manager.ReturnToLoadingProjects();
        }
       
    }
}
