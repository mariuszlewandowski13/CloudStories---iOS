using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectsMenu : MonoBehaviour {

    public ProjectsPanelManager man;

    public string projectsOwner;
    private bool isLoaded;
    public GameObject createnewProjectIconPrefab;

    public CheckRaycasting firstIcon;
    public CheckRaycasting lastIcon;

    public VerticalScroll scroll;

    public GameObject iconPrefab;

    public Transform panel;

    protected float startX = 0.07f;
    protected float startY = 0.85f;
    protected float startZ = -6.0f;

    protected float xAdding = 0.3f;
    protected float yAdding = -0.2f;

    protected int colsCount = 3;

    protected float actualX = 0.07f;
    protected float actualY = 0.85f;
    protected float actualZ = -6.0f;

    private void Update()
    {
        if (firstIcon == null || (firstIcon.isRaycasting && scroll.canScrollUp))
        {
            scroll.canScrollUp = false;
        }
        else if (!firstIcon.isRaycasting && !scroll.canScrollUp)
        {
            scroll.canScrollUp = true;
        }
        if (lastIcon == null || (lastIcon.isRaycasting && scroll.canScrollDown))
        {
            scroll.canScrollDown = false;
        }
        else if (!lastIcon.isRaycasting && !scroll.canScrollDown)
        {
            scroll.canScrollDown = true;
        }

        if (!isLoaded && ProjectsManager.projectsLoaded)
        {
            isLoaded = true;
            LoadScenes();
        }

    }

    protected void ClearIcons()
    {

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        actualX = startX;
        actualY = startY;
        actualZ = startZ;
    }

    private void OnEnable()
    {
        isLoaded = false;
        ClearButtons();
    }

    public void LoadScenes()
    {
        scroll = GetComponent<VerticalScroll>();
        startY = actualY = 0.4f;
        startX = actualX = -0.3f;
        ShowUProjectScenes();
    }

    public void ShowUProjectScenes()
    {
        int i = 0;

        ClearButtons();
 
        foreach (ProjectData project in ProjectsManager.projects)
        {
            
            if ((projectsOwner == ""  && ApplicationStaticData.appOwner != project.owner) || projectsOwner == project.owner)
            {
                GameObject newButton = Instantiate(iconPrefab);
                newButton.transform.parent = scroll.transform;

                newButton.transform.position = scroll.transform.position;
                newButton.transform.localPosition += new Vector3(actualX, actualY, actualZ);

                if (project.color == new Color())
                {
                    project.color = RandomColor();
                }

                newButton.GetComponent<ProjectLoadBtn>().SetProjectNumber(project.ID, project.color, man);

                i++;

                if (i == ProjectsManager.projects.Count)
                {
                    CheckRaycasting raycasting = newButton.AddComponent<CheckRaycasting>();
                    raycasting.raycastingGameObject = panel;
                    lastIcon = raycasting;
                }else if (i == 1)
                {
                    CheckRaycasting raycasting = newButton.AddComponent<CheckRaycasting>();
                    raycasting.raycastingGameObject = panel;
                    firstIcon = raycasting;
                }

                IconShowHideScript showHide = newButton.AddComponent<IconShowHideScript>();
                showHide.SetReferenceObject(panel);

                if (i % colsCount == 0)
                {

                    actualX = startX;
                    actualY += yAdding;
                }
                else
                {
                    actualX += xAdding;
                }
            }

        }

       

    }

    public void ClearButtons()
    {
        if (scroll != null)
        {
            foreach (Transform child in scroll.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public Color RandomColor()
    {
        return new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), 1.0f);
    }
}
