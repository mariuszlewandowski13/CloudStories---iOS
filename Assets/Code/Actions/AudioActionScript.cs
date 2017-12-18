using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;
using System.Threading;



public class AudioActionScript : ActionScript {

    private bool loading;
    private bool loadingFromURL;

    private string objID;

    public override void SetActionData(string data)
    {
        if (data != actionData && !loading)
        {
            objID = GetComponent<ObjectActionsScript>().objectID;
            loading = true;
            base.SetActionData(data);
            CreateAudioSource();
            loadingFromURL = true;

            //Thread th = new Thread(LoadAudio);
            //th.Start();
            LoadAudio();

        }

    }

    private void LoadAudio()
    {
        WebRequest request = WebRequest.Create("http://vrowser.e-kei.pl/CloudStoriesImages/Project" + ApplicationStaticData.projectID.ToString() +"/SCENE"+
            ApplicationStaticData.sceneID.ToString() + "/objects/"+ objID + "/"+actionData);
        // If required by the server, set the credentials.  
        request.Credentials = CredentialCache.DefaultCredentials;
        // Get the response.  
        WebResponse response = request.GetResponse();
        // Display the status.  
        Debug.Log(((HttpWebResponse)response).StatusDescription);
        // Get the stream containing content returned by the server.  
        Stream dataStream = response.GetResponseStream();

        byte [] audioBytes = ImageScript.StreamToByteArray(dataStream);

        string tempFile = Application.persistentDataPath + "/bytes.mp3";
        System.IO.File.WriteAllBytes(tempFile, audioBytes);

        loadingFromURL = false;
    }

    private void Update()
    {
        if (loading && !loadingFromURL)
        {
           StartCoroutine(LoadFromBytes());
        }
    }

    IEnumerator LoadFromBytes()
    {
       
        string tempFile = Application.persistentDataPath + "/bytes.mp3";
        Debug.Log(tempFile);
        WWW loader = new WWW("file://" + tempFile);
        yield return loader;
        if (!System.String.IsNullOrEmpty(loader.error))
            Debug.LogError(loader.error);
        else {
            AudioClip s1 = loader.GetAudioClip(false, false, AudioType.MPEG);
            gameObject.GetComponent<AudioSource>().clip = s1;
            gameObject.GetComponent<AudioSource>().Play();
        }
        loading = false;
    }

    private void CreateAudioSource()
    {
        if (GetComponent<AudioSource>() == null)
        {
            AudioSource audio = gameObject.AddComponent<AudioSource>();
            audio.maxDistance = 1.0f;
            audio.minDistance = 0.0f;
        }
        else {
            gameObject.GetComponent<AudioSource>().Stop();
        }
    }
}
