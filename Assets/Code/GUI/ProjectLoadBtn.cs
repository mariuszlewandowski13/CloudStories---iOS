using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectLoadBtn : ClickableButton, IClickable {

    private int projectNumber;

    private Color color;

    private ProjectsPanelManager manager;

    public void SetProjectNumber(int number, Color col, ProjectsPanelManager man)
    {
        projectNumber = number;
        color = col;
        manager = man;
        UpdatePresention();
        
    }

    private void UpdatePresention()
    {
        transform.Find("Text").GetComponent<TextMesh>().text = projectNumber.ToString();
        GetComponent<Renderer>().material.color = color;
    }

  

    public void Clicked()
    {
        manager.LoadProject(projectNumber);
    }

}
