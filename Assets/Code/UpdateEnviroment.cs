using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
            WWWForm form = new WWWForm();
            WWW w = new WWW("https://vrowser.e-kei.pl/CloudStories/" + "GetProjectData.php", form);
            StartCoroutine(request(w));

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

    private void ProcessLine(string[] line)
    {
        if (line[1] == "3DObj")
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
