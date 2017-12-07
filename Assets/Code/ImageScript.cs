#region Usings

using UnityEngine;
using System;
using System.Collections;
using System.Threading;
using System.Net;
using System.Runtime.InteropServices;
using System.IO;
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

        LoadTexture();
    }

    void Update()
    {
        if (textureReady)
        {
            GetComponent<Renderer>().material.mainTexture = tex;
            textureReady = false;
        }

    }


    
    public void SetImagePath(string path)
    {
        imagePath = path;
        LoadImgAsMaterial();
    }

    void LoadTexture()
    {
        if (tex == null)
        {
            try
            {
                tex = new Texture2D(2, 2);
                // Create a request for the URL.   
                WebRequest request = WebRequest.Create(imagePath);
                // If required by the server, set the credentials.  
                request.Credentials = CredentialCache.DefaultCredentials;
                // Get the response.  
                WebResponse response = request.GetResponse();
                // Display the status.  
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.  
                Stream dataStream = response.GetResponseStream();

                tex.LoadImage(StreamToByteArray(dataStream));
                // Read the content.  

                textureReady = true;
            }
            catch (Exception e)
            {
                Destroy(gameObject, 0.5f);
            }
           

            Debug.Log(imagePath);

        }
        else {
            textureReady = true;
        }
       
    }

    public static byte[] StreamToByteArray(Stream stream)
    {
        long originalPosition = 0;

        if (stream.CanSeek)
        {
            originalPosition = stream.Position;
            stream.Position = 0;
        }

        try
        {
            byte[] readBuffer = new byte[4096];

            int totalBytesRead = 0;
            int bytesRead;

            while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
            {
                totalBytesRead += bytesRead;

                if (totalBytesRead == readBuffer.Length)
                {
                    int nextByte = stream.ReadByte();
                    if (nextByte != -1)
                    {
                        byte[] temp = new byte[readBuffer.Length * 2];
                        Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                        Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                        readBuffer = temp;
                        totalBytesRead++;
                    }
                }
            }

            byte[] buffer = readBuffer;
            if (readBuffer.Length != totalBytesRead)
            {
                buffer = new byte[totalBytesRead];
                Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
            }
            return buffer;
        }
        finally
        {
            if (stream.CanSeek)
            {
                stream.Position = originalPosition;
            }
        }
    }


    #endregion
}
