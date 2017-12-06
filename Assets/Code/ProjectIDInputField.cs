using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectIDInputField : MonoBehaviour {

    public Dropdown dropdown;
    private InputField input;
    public Button okButton;

    void Start()
    {
        input = GetComponent<InputField>();
        input.onValueChanged.AddListener(UpdateInfo);
    }

    void UpdateInfo(string arg)
    {
        dropdown.options.Clear();
        ProjectData projData = null;

        int actualProject = int.Parse(input.text);

        foreach (ProjectData data in ProjectsManager.projects)
        {
            if (data.ID == actualProject)
            {
                
                projData = data;
                break;
            }
        }

        if (projData != null)
        {
            dropdown.interactable = true;
            foreach (int scene in projData.sceneNumbers)
            {
                dropdown.options.Add(new Dropdown.OptionData(scene.ToString()));
            }
            okButton.interactable = true;
        }
        else {
            dropdown.interactable = false;
            okButton.interactable = false;
        }
    }
}
