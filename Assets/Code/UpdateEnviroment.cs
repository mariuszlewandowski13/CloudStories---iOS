using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.IO;
public class UpdateEnviroment : MonoBehaviour {

    private bool updating;
    private string message;

    private int layout = 0;

    private List<int> objectsNumbers;

    public GameObject[] layouts;
    public GameObject[] objectsPrefabs;

    public GameObject layoutObject;

	void Start () {
        objectsNumbers = new List<int>();
        InvokeRepeating("UpdateEnvir", 5.0f, 5.0f);
	}

    private void UpdateEnvir()
    {
        if (!updating)
        {
            updating = true;
            //WWWForm form = new WWWForm();
            //form.AddField("ID", ApplicationStaticData.projectID);
            //WWW w = new WWW("https://vrowser.e-kei.pl/CloudStories/" + "GetProjectData.php", form);
            //StartCoroutine(request(w));
            //WWWForm form = new WWWForm();
            //WWW w = new WWW("https://vrowser.e-kei.pl/CloudStories/" + "GetProjectData.php", form);
            //StartCoroutine(request(w));
            Upload();

        }
    }

    IEnumerator request(WWW w)
    {
        yield return w;
        if (w.error == null)
        {
            message = w.text;
        }
        else
        {
            message = "ERROR: " + w.error + "\n";
        }
        string [] res;
        string [] msg = message.Split(new string[] { "@@@@@" }, StringSplitOptions.None);
        foreach (string row in msg)
        {
            res = row.Split(new string[] { "#####" }, StringSplitOptions.None);
            if (res.Length > 1)
            {
                ProcessLine(res);
            }
        }

        Debug.Log(message);
        updating = false;
    }

    void Upload()
    {
        // Create a request for the URL.   
        WebRequest request = WebRequest.Create(
          "http://vrowser.e-kei.pl/CloudStories/" + "GetProjectData.php?ID=" + ApplicationStaticData.projectID.ToString());
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
                ProcessLine(res);
            }
        }

        // Display the content.  
        Debug.Log(responseFromServer);
        // Clean up the streams and the response.  
        reader.Close();
        response.Close();

        updating = false;
    }

    private void ProcessLine(string[] line)
    {
        if (line[1] == "3DObject")
        {
            int objectNumber = Int32.Parse(line[0]);
            if (!objectsNumbers.Contains(objectNumber))
            {
                objectsNumbers.Add(objectNumber);
                GameObject prefab = objectsPrefabs[Int32.Parse(line[2])];
                Vector3 pos = new Vector3(float.Parse(line[3]), float.Parse(line[4]), float.Parse(line[5]));
                Instantiate(prefab, pos, new Quaternion());

            }
        }
        else if (line[1] == "LAYOUT")
        {
            int layoutNumber = Int32.Parse(line[2]);
            if (layout != layoutNumber)
            {
                layout = layoutNumber;
                ClearLayout();
                if (layoutNumber != 0)
                {
                   GameObject newLayout = Instantiate(layouts[layoutNumber - 1]);
                    newLayout.transform.parent = layoutObject.transform;
                }
                
            }
        }


    }


    private void ClearLayout()
    {
        if (layoutObject != null)
        {
            foreach (Transform child in layoutObject.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }



}
