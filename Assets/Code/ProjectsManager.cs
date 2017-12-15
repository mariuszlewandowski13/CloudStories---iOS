using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.IO;
using System.Threading;

public class ProjectData
{
    public int ID;
    public string owner;
    public List<int> sceneNumbers;

    public Color color;

    public ProjectData(int id, string owner)
    {
        ID = id;
        this.owner = owner;
        sceneNumbers = new List<int>();
    }

    
}


public class ProjectsManager : MonoBehaviour {

    public static List<ProjectData> projects;

    public static bool projectsLoaded;

    public GameObject ownerPanel;

    void Start () {
        projects = new List<ProjectData>();
        Thread thr2 = new Thread(LoadProjects);
        thr2.Start();
        SetOwnerPanelName();
    }

    void LoadProjects()
    {
        // Create a request for the URL.   
        WebRequest request = WebRequest.Create(
          "http://vrowser.e-kei.pl/CloudStories/" + "LoadProjectsData.php");
        // If required by the server, set the credentials.  
        request.Credentials = CredentialCache.DefaultCredentials;
        // Get the response.  
        WebResponse response = request.GetResponse();
        // Display the status.  
        Console.WriteLine(((HttpWebResponse)response).StatusDescription);
        // Get the stream containing content returned by the server.  
        Stream dataStream = response.GetResponseStream();
        // Open the stream using a StreamReader for easy access.  
        StreamReader reader = new StreamReader(dataStream);
        // Read the content.  
        string responseFromServer = reader.ReadToEnd();
        string[] res;
        string[] msg = responseFromServer.Split(new string[] { "@@@@@" }, StringSplitOptions.None);
        foreach (string row in msg)
        {
            res = row.Split(new string[] { "#####" }, StringSplitOptions.None);
            if (res.Length > 1)
            {
                ProjectData proj = new ProjectData(int.Parse(res[0]), res[1]);
                string [] scenes = res[2].Split(new string[] {  ","}, StringSplitOptions.None);
                foreach (string scene in scenes)
                {
                    int number;
                    if (int.TryParse(scene, out number))
                        {
                        proj.sceneNumbers.Add(number);
                    }
                    
                }
                projects.Add(proj);

            }
        }

        // Display the content.  
        Debug.Log(responseFromServer);
        // Clean up the streams and the response.  
        reader.Close();
        response.Close();

        

        projectsLoaded = true;
    }

    private void SetOwnerPanelName()
    {
        if (ownerPanel != null)
        {
            ownerPanel.GetComponent<ProjectsMenu>().projectsOwner = ApplicationStaticData.appOwner;
        }
    }
}
