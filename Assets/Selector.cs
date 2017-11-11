using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector:MonoBehaviour
{
    private float hoover;
    private int cnt = 0;
    public GameObject[] rows;
    public GameObject content;
    public bool ready=true;

    public float Hoover
        {
            get
            {
                return hoover;
            }

            set
            {
                hoover = value;
                fadeIn(hoover);
            }
        }
    



    private void fadeIn(float hoover)
    {
        Color color = GetComponent<Renderer>().materials[0].GetColor("_Color");
        color.a = hoover;
        GetComponent<Renderer>().material.color = color;
    }

    IEnumerator fadeOut()
    {
        while (Hoover>0.01f)
        {
            Hoover -= 0.008f*Time.time;
            yield return null;
            if (Hoover == 0) StopCoroutine("fadeOut");
        }
    }

    IEnumerator moveRowsBelowDown() {
        while (cnt < 25)
        {
            ready = false;
            foreach (GameObject row in rows)
            {
                row.transform.position = new Vector3(row.transform.position.x, row.transform.position.y-cnt*0.000275f, row.transform.position.z);
                yield return null;
            }
            cnt++;
            Debug.Log(cnt);
        }
        cnt = 0;
        content.SetActive(true);
        ready = true;
    }

    IEnumerator moveRowsBelowUp()
    {
        content.SetActive(false);
        while (cnt < 25)
        {
            ready = false;
            foreach (GameObject row in rows)
            {
                row.transform.position = new Vector3(row.transform.position.x, row.transform.position.y + cnt * 0.000275f, row.transform.position.z);
                yield return null;
            }
            cnt++;
            Debug.Log(cnt);
        }
        cnt = 0;
        ready = true;
    }





}

