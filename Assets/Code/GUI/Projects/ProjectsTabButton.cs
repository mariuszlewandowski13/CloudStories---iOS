using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectsTabButton : MonoBehaviour
{

    public int tabToLoad;
    public ProjectsPanelManager manager;

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(LoadTab);
    }

    void LoadTab()
    {
        manager.TabButtonClicked(tabToLoad);
    }
}
