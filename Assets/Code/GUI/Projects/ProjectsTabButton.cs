using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectsTabButton : ClickableButton,IClickable
{
    public int tabToLoad;
    public ProjectsPanelManager manager;

    public bool setInteractable;

    private void Start()
    {
        if (setInteractable)
        {
            interactable = true;
        }
    }


    public void Clicked()
    {
        if (interactable)
        {
            manager.TabButtonClicked(tabToLoad);
        }
    }
}
