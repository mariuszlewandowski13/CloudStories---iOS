using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.IO;

public enum ObjectsTypes
{
    shapeObject,
    object3D,
    movie,
    gif,
    audio,
    layout
}

public class UpdateEnviroment : MonoBehaviour {

    public static UpdateEnviroment instance;

    public GameObject spawnedObjectsParent;
    private string objectsPath = "obj3D";

    private bool updating;
    private string message;

    private int layout = 0;

    private Dictionary<int, GameObject> objectsNumbers;

    public GameObject[] layouts;
    public GameObject[] objectsPrefabs;
    public GameObject[] shapesPrefabs;

    public GameObject object3DPrefab;

    public GameObject shapeObjectPrefab;
    public GameObject videoObjectPrefab;

    public GameObject layoutObject;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void LoadObjects()
    {
        objectsNumbers = new Dictionary<int, GameObject>();
        InvokeRepeating("UpdateEnvir", 1.0f, 5.0f);
    }

    public void StopLoading()
    {
       CancelInvoke("UpdateEnvir");
        ClearEnviroment();
    }

    public void ChangeSceneNumber(int sceneNumber)
    {
        StopLoading();
        ApplicationStaticData.sceneID = sceneNumber;
        InvokeRepeating("UpdateEnvir", 0.0f, 5.0f);
    }

    private void ClearEnviroment()
    {
        foreach (int key in objectsNumbers.Keys)
        {
            Destroy(objectsNumbers[key]);
        }
        objectsNumbers.Clear();

    }


    private void UpdateEnvir()
    {
        if (!updating)
        {
            updating = true;
            
            Download();

        }
    }

 

    void Download()
    {
        // Create a request for the URL.   
        WebRequest request = WebRequest.Create(
          "http://vrowser.e-kei.pl/CloudStories/" + "GetSceneData.php?projectID=" + ApplicationStaticData.projectID.ToString()+ "&sceneID=" + ApplicationStaticData.sceneID.ToString());
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
        string objType = line[1];

        if (objType == "Object3D")
        {
            int objectNumber = Int32.Parse(line[0]);
            if (!objectsNumbers.ContainsKey(objectNumber))
            {

                GameObject newObject = null;


                Vector3 pos = new Vector3(float.Parse(line[5]), float.Parse(line[6]), float.Parse(line[7]));
                Vector3 rot = new Vector3(float.Parse(line[8]), float.Parse(line[9]), float.Parse(line[10]));
                Vector3 size = new Vector3(float.Parse(line[11]), float.Parse(line[12]), float.Parse(line[13]));
                Color color = new Color(float.Parse(line[14]), float.Parse(line[15]), float.Parse(line[16]), float.Parse(line[17]));

                newObject = Load3DObject(line[4], line[3], pos, rot, objectNumber, color);

                newObject.transform.parent = spawnedObjectsParent.transform;
                newObject.transform.localPosition = pos;
                newObject.transform.localScale = size;

            //    if (line.Length > 12)
            //    { 
            //    ObjectActionsScript actions = newObject.AddComponent<ObjectActionsScript>();
            //    string[] res = line[12].Split(new string[] { "*****" }, StringSplitOptions.None);
            //    actions.ProcessLines(res, line[0]);
            //}
                objectsNumbers.Add(objectNumber, newObject);

                

            }
            else {
                GameObject objectToChange = objectsNumbers[objectNumber];

                Vector3 pos = new Vector3(float.Parse(line[5]), float.Parse(line[6]), float.Parse(line[7]));
                Vector3 rot = new Vector3(float.Parse(line[8]), float.Parse(line[9]), float.Parse(line[10]));
                Vector3 size = new Vector3(float.Parse(line[11]), float.Parse(line[12]), float.Parse(line[13]));

                //if (line.Length > 12)
                //{
                //    ObjectActionsScript actions = objectToChange.GetComponent<ObjectActionsScript>();
                //    string[] res = line[12].Split(new string[] { "*****" }, StringSplitOptions.None);
                //    actions.ProcessLines(res, line[0]);
                //}

                objectToChange.transform.localPosition = pos;
                objectToChange.transform.rotation = Quaternion.Euler(rot);

               // objectToChange.transform.parent = null;
                objectToChange.transform.localScale = size;
                //objectToChange.transform.parent = spawnedObjectsParent.transform;
            }
        }
        else if (objType == "ImageObject")
        {
            int objectNumber = Int32.Parse(line[0]);
            if (!objectsNumbers.ContainsKey(objectNumber))
            {

                GameObject prefab = shapeObjectPrefab;




                Vector3 pos = new Vector3(float.Parse(line[5]), float.Parse(line[6]), float.Parse(line[7]));
                Vector3 rot = new Vector3(float.Parse(line[8]), float.Parse(line[9]), float.Parse(line[10]));
                Vector3 size = new Vector3(float.Parse(line[11]), float.Parse(line[12]), float.Parse(line[13]));

                GameObject newShape = null;

                    newShape = Instantiate(prefab, pos, Quaternion.Euler(rot));
                    newShape.GetComponent<ImageScript>().SetImagePath(line[3]);

                //if (line.Length > 12)
                //{
                //    ObjectActionsScript actions = newShape.AddComponent<ObjectActionsScript>();
                //    string[] res = line[12].Split(new string[] { "*****" }, StringSplitOptions.None);
                //    actions.ProcessLines(res, line[0]);
                //}



                newShape.transform.parent = spawnedObjectsParent.transform;
                newShape.transform.localPosition = pos;
                newShape.transform.localScale = size;
               
                objectsNumbers.Add(objectNumber, newShape);
            }
            else {
                try
                {
                    GameObject objectToChange = objectsNumbers[objectNumber];

                    Vector3 pos = new Vector3(float.Parse(line[5]), float.Parse(line[6]), float.Parse(line[7]));
                    Vector3 rot = new Vector3(float.Parse(line[8]), float.Parse(line[9]), float.Parse(line[10]));
                    Vector3 size = new Vector3(float.Parse(line[11]), float.Parse(line[12]), float.Parse(line[13]));


                    //if (line.Length > 12)
                    //{
                    //    ObjectActionsScript actions = objectToChange.GetComponent<ObjectActionsScript>();
                    //    string[] res = line[12].Split(new string[] { "*****" }, StringSplitOptions.None);
                    //    actions.ProcessLines(res, line[0]);
                    //    }

                    objectToChange.transform.localPosition = pos;
                    objectToChange.transform.rotation = Quaternion.Euler(rot);
                    objectToChange.transform.localScale = size;
                }
                catch (Exception e)
                {
                    
                }
            }
        }
        //else if (objType == ObjectsTypes.movie)
        //{
        //    int objectNumber = Int32.Parse(line[0]);
        //    if (!objectsNumbers.ContainsKey(objectNumber))
        //    {

        //        GameObject prefab = videoObjectPrefab;




        //        Vector3 pos = new Vector3(float.Parse(line[3]), float.Parse(line[4]), float.Parse(line[5]));
        //        Vector3 rot = new Vector3(float.Parse(line[6]), float.Parse(line[7]), float.Parse(line[8]));
        //        Vector3 size = new Vector3(float.Parse(line[9]), float.Parse(line[10]), float.Parse(line[11]));

        //        GameObject newVideo = null;

                

        //            newVideo = Instantiate(prefab, pos, Quaternion.Euler(rot));

        //        if (line.Length > 12)
        //        {
        //            ObjectActionsScript actions = newVideo.AddComponent<ObjectActionsScript>();
        //            string[] res = line[12].Split(new string[] { "*****" }, StringSplitOptions.None);
        //            actions.ProcessLines(res, line[0]);
        //        }



        //        newVideo.transform.parent = spawnedObjectsParent.transform;
        //        newVideo.transform.localPosition = pos;
        //        newVideo.transform.localScale = size;
        //        newVideo.GetComponent<MediaPlayerCtrl>().m_strFileName =  line[2];

        //        objectsNumbers.Add(objectNumber, newVideo);
        //    }
        //    else
        //    {
        //        GameObject objectToChange = objectsNumbers[objectNumber];

        //        Vector3 pos = new Vector3(float.Parse(line[3]), float.Parse(line[4]), float.Parse(line[5]));
        //        Vector3 rot = new Vector3(float.Parse(line[6]), float.Parse(line[7]), float.Parse(line[8]));
        //        Vector3 size = new Vector3(float.Parse(line[9]), float.Parse(line[10]), float.Parse(line[11]));


        //        if (line.Length > 12)
        //        {
        //            ObjectActionsScript actions = objectToChange.GetComponent<ObjectActionsScript>();
        //            string[] res = line[12].Split(new string[] { "*****" }, StringSplitOptions.None);
        //            actions.ProcessLines(res, line[0]);
        //        }

        //        objectToChange.transform.localPosition = pos;
        //        objectToChange.transform.rotation = Quaternion.Euler(rot);
        //        objectToChange.transform.localScale = size;
        //    }
        //}


    }


    private GameObject Load3DObject(string MeshPath, string texPath, Vector3 pos, Vector3 rot, int id, Color color)
    {
        GameObject newObject = Instantiate(object3DPrefab, pos, Quaternion.Euler(rot));
        newObject.GetComponent<Object3DScript>().LoadObject(MeshPath, texPath, id, color); 
        return newObject;
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
