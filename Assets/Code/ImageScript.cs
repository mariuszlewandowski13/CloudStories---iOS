#region Usings

using UnityEngine;
using System;
using System.Collections;
using System.Threading;
using System.Net;
using System.Runtime.InteropServices;

#endregion


[RequireComponent(typeof(Renderer))]
[Serializable]
public class ImageScript : MonoBehaviour {

    #region Public Properties

    public string imagePath;
    private bool textureReady;

    private Texture2D tex;


    #endregion

    #region Methods

    private void LoadImgAsMaterial()
    {
        StartCoroutine(LoadTexture(imagePath));
    }

    void Update()
    {
        if (textureReady)
        {
           // Debug.Log("asdasd");
            GetComponent<Renderer>().material.mainTexture = tex;
            textureReady = false;
        }

    }


    
    public void SetImagePath(string path)
    {
        imagePath = path;
        LoadImgAsMaterial();
    }

    IEnumerator LoadTexture(string name)
    {
        if (tex == null)
        {
           tex = new Texture2D(2, 2);
                WWW www = new WWW(name);
                yield return www;

               // Debug.Log(name);

                if (www.error != null)
                {
                   // ObjectSpawnerScript.DestroyGameObject(gameObject, true);
                }
                else {
                      try
                      {
                        www.LoadImageIntoTexture(tex);
                        textureReady = true;
                        //Thread th = new Thread(LoadTextureFromUrl);
                        //th.Start();
                    }
                      catch (Exception e)
                      {
                           // ObjectSpawnerScript.DestroyGameObject(gameObject);
                            Debug.Log(e);
                      }
                }
                www.Dispose();         
            

        }
        else {
            textureReady = true;
        }
       
    }


    #endregion
}
