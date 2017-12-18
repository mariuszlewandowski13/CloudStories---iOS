using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBase : MonoBehaviour {


    private bool active;

    protected Vector3 hitPoint;

    protected void CursorOn()
    {
        active = true;
        Vector3[] points = new Vector3[] { transform.position, hitPoint };
        GetComponent<LineRenderer>().positionCount = 2;
        GetComponent<LineRenderer>().SetPositions(points);
    }

    protected void CursorOff()
    {
        if (active)
        {
            active = false;
            GetComponent<LineRenderer>().positionCount = 0;
        }

    }
}
