using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectLoadBtn : MonoBehaviour {

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
        transform.Find("Text").GetComponent<Text>().text = projectNumber.ToString();
        GetComponent<Image>().color = color;
    }

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(LoadProj);
    }

    void LoadProj()
    {
        manager.LoadProject(projectNumber);
    }

}
