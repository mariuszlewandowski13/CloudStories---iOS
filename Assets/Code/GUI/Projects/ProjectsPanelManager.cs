using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectsPanelManager : MonoBehaviour {

    public GameObject[] tabs;
    public ClickableButton[] tabsButtons;

    public GameObject panel;

    public UpdateEnviroment envir;

    public GameObject backButton;

    public void TabButtonClicked(int tabNumberToActivate)
    {
        ActivateTab(tabNumberToActivate);
        ActivateButtons(tabNumberToActivate);
    }

    private void ActivateTab(int number)
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].SetActive(false);
        }
        tabs[number].SetActive(true);
    }

    private void ActivateButtons(int number)
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            tabsButtons[i].interactable = true;
        }
        tabsButtons[number].interactable = false;
    }

    public void LoadProject(int number)
    {
        panel.SetActive(false);
        ApplicationStaticData.projectID = number;
        envir.LoadObjects();
        backButton.SetActive(true);
    }

    public void ReturnToLoadingProjects()
    {
        envir.StopLoading();
        panel.SetActive(true);
        backButton.SetActive(false);
    }


}
