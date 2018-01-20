using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.IO;
using System.Threading;



public class Object3Dpart
{
    public string meshPath;
    public string texturePath;

    public Color color;

    public byte[] texBytes;
    public byte[] objBytes;

    public bool TexBytesLoaded;
    public bool MeshBytesLoaded;

    public GameObject ownerObject;

    public Object3Dpart(string mp, string tp, Color c, GameObject owner)
    {
        meshPath = mp;
        texturePath = tp;
        color = c;
        ownerObject = owner;
    }

}


public class Object3DScript : MonoBehaviour {

    private List<Object3Dpart> parts;

    private object partsLock = new object();

    private int ID;

    private static string dataPath;

    public void LoadObject(string mP, string tP,  int id, Color color)
    {
        parts = new List<Object3Dpart>();
        string[] meshDatastring = tP.Split(new string[] { "." }, StringSplitOptions.None);
        Debug.Log(meshDatastring[meshDatastring.Length - 1]);
        if (meshDatastring[meshDatastring.Length - 1] != "txt")
        {
            parts.Add(new Object3Dpart(mP, tP, color, gameObject));

            Thread thr2 = new Thread(() => LoadMesh(parts[0]));
            thr2.Start();


            Thread thr = new Thread(() => Loadtexture(parts[0]));
            thr.Start();
        }
        else {
            //Thread thr = new Thread(() => LoadData(mP, id));
            // thr.Start();
            LoadData(tP, id);
        }


    }

    private void LoadData(string meshPath, int id)
    {
        //try
//{
            string mainpath = "http://vrowser.e-kei.pl/CloudStoriesImages/Project"+ApplicationStaticData.projectID+"/SCENE"+ApplicationStaticData.sceneID+"/objects/"+id.ToString()+"/";
            WebRequest request = WebRequest.Create(meshPath);
            // If required by the server, set the credentials.  
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            StreamReader dataStream = new StreamReader(response.GetResponseStream());
            string line = dataStream.ReadToEnd();

            string[] lines = line.Split(new string[] { "\n" }, StringSplitOptions.None);

            foreach (string li in lines)
            {
            if (li != "")
            {
                ProcessMeshesLine(li, mainpath);
            }
                
            }


        //}
        //catch (Exception e)
        //{
        //    Debug.Log("Cannot load meshes");
        //}

    }

    private void ProcessMeshesLine(string line, string mainPath)
    {
        string[] fields = line.Split(new string[] { " " }, StringSplitOptions.None);
        
        string meshPath = mainPath + "Object3D" + fields[0] + ".obj";
        string texPath = "";
        if (fields[1] != "-1")
        {
             texPath = mainPath + "Object3D" + fields[1] + ".png";
        }

        Color color = new Color(float.Parse(fields[2]), float.Parse(fields[3]), float.Parse(fields[4]), float.Parse(fields[5]));

        GameObject gm = Instantiate(UpdateEnviroment.instance.object3DPrefab, transform.position, transform.rotation);

        gm.transform.parent = transform;

        lock (partsLock)
        {
            Object3Dpart part = new Object3Dpart(meshPath, texPath, color, gm);
            parts.Add(part);


            Thread thr2 = new Thread(() => LoadMesh(part));
            thr2.Start();
            //LoadMesh(part);

            Thread thr = new Thread(() => Loadtexture(part));
            thr.Start();
        }
    }

    private void Start()
    {
        dataPath = Application.persistentDataPath;
    }

    private void Update()
    {
        lock (partsLock)
        {
            if (parts != null)
            {
              //  Debug.Log(parts.Count);
                for (int i = 0; i < parts.Count; i++)
                {
                    if (parts[i].TexBytesLoaded)
                    {
                        parts[i].TexBytesLoaded = false;
                        ShowTexture(parts[i]);
                    }

                    if (parts[i].MeshBytesLoaded)
                    {
                        parts[i].MeshBytesLoaded = false;
                        ShowObj(parts[i]);
                    }
                }
            }
            
        }     
    }

    private void ShowTexture(Object3Dpart part)
    {
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(part.texBytes);
        part.ownerObject.GetComponent<Renderer>().material.mainTexture = tex;
    }

    private void ShowObj(Object3Dpart part)
    {
        Mesh mesh = new Mesh();
        mesh = FastObjImporter.Instance.ImportFile(part.objBytes);
        part.ownerObject.GetComponent<MeshFilter>().mesh= mesh;
        //part.ownerObject.AddComponent<BoxCollider>();
        part.ownerObject.GetComponent<Renderer>().material.color = part.color;
    }



    private void Loadtexture(Object3Dpart part)
    {
        if (part.texturePath != "")
        {
            try
            {
                WebRequest request = WebRequest.Create(part.texturePath);
                // If required by the server, set the credentials.  
                request.Credentials = CredentialCache.DefaultCredentials;
                // Get the response.  
                WebResponse response = request.GetResponse();
                // Display the status.  
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.  
                Stream dataStream = response.GetResponseStream();

                part.texBytes = ImageScript.StreamToByteArray(dataStream);

                part.TexBytesLoaded = true;
            }
            catch (Exception e)
            {
                Debug.Log("Cannot load texture");
            }
        }
    }

    private void LoadMesh(Object3Dpart part)
    {
        WebRequest request = WebRequest.Create(part.meshPath);
        // If required by the server, set the credentials.  
        request.Credentials = CredentialCache.DefaultCredentials;
        // Get the response.  
        WebResponse response = request.GetResponse();
        // Display the status.  
        Console.WriteLine(((HttpWebResponse)response).StatusDescription);
        // Get the stream containing content returned by the server.  
        Stream dataStream = response.GetResponseStream();

        part.objBytes = ImageScript.StreamToByteArray(dataStream);


        part.MeshBytesLoaded = true;
    }

}
