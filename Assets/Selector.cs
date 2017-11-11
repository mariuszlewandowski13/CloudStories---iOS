using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector:MonoBehaviour
{
    private float hoover;

    public float Hoover
        {
            get
            {
                return hoover;
            }

            set
            {
                hoover = value;
                updateOpacity(hoover);
            }
        }

    private void updateOpacity(float hoover)
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

}

