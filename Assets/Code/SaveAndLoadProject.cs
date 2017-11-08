using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class SaveAndLoadProject : MonoBehaviour {

    public Text text;
    public Text errorText;
    public 
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(CreateObject);
    }

    void CreateObject()
    {
        string tex = text.text;
        errorText.text = "";
        int ID;
        if (tex != "" && Int32.TryParse(tex, out ID))
        {
            ApplicationStaticData.projectID = ID;
            SceneManager.LoadScene("ArScene");
        }
        else
        {
            errorText.text = "Insert valid project ID!!!";
        }
    }
}
