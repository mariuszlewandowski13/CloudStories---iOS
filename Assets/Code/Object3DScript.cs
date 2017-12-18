using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.IO;
using System.Threading;


public class Object3DScript : MonoBehaviour {

    private string path;
    private string objName;
    private string texName;

    private byte[] texBytes;
    private byte[] objBytes;

    private bool TexBytesLoaded;
    private bool MeshBytesLoaded;

    private int ID;

    private static string dataPath;

    public void LoadObject(string path, string objnam, string texNam, int id)
    {
        this.path = path;
        objName = objnam;
        texName = texNam;
        ID = id;

        Thread thr2 = new Thread(LoadMesh);
        thr2.Start();

        Thread thr = new Thread(Loadtexture);
        thr.Start();
    }

    private void Start()
    {
        dataPath = Application.persistentDataPath;
    }

    private void Update()
    {
        if (TexBytesLoaded)
        {
            TexBytesLoaded = false;
            ShowTexture();
            
        }

        if (MeshBytesLoaded)
        {
            MeshBytesLoaded = false;
            ShowObj();
        }
    }

    private void ShowTexture()
    {
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(texBytes);
        GetComponent<Renderer>().material.mainTexture = tex;
    }

    private void ShowObj()
    {
        Mesh mesh = new Mesh();

        mesh = FastObjImporter.Instance.ImportFile(objBytes);
        GetComponent<MeshFilter>().mesh= mesh;
        gameObject.AddComponent<BoxCollider>();
    }



    private void Loadtexture()
    {
        try
        {
            WebRequest request = WebRequest.Create(path + texName);
            // If required by the server, set the credentials.  
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            Stream dataStream = response.GetResponseStream();

            texBytes = ImageScript.StreamToByteArray(dataStream);

            TexBytesLoaded = true;
        }
        catch (Exception e)
        {
            Debug.Log("Cannot load texture");
        }
    }

    private void LoadMesh()
    {
       
        WebRequest request = WebRequest.Create(path + objName);
        // If required by the server, set the credentials.  
        request.Credentials = CredentialCache.DefaultCredentials;
        // Get the response.  
        WebResponse response = request.GetResponse();
        // Display the status.  
        Console.WriteLine(((HttpWebResponse)response).StatusDescription);
        // Get the stream containing content returned by the server.  
        Stream dataStream = response.GetResponseStream();

        objBytes = ImageScript.StreamToByteArray(dataStream);


        MeshBytesLoaded = true;
    }

}
