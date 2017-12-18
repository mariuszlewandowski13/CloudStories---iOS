using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingScript : MonoBehaviour {

    private RaycastHit hit;

    private Vector3 actualPoint;
    private Vector3 prevoiusPoint;

    public Transform actualPointing;

    public bool isActive = true;

    public bool scrollingStarted;

    void Update()
    {
        bool pressedDown = Input.GetMouseButton(0);


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);



        if (isActive && !scrollingStarted)
        {

            if (pressedDown && hit.transform != null && hit.transform.GetComponent<IScrollable>() != null && hit.transform.tag == "Scroll")
            {
                actualPoint = prevoiusPoint = Input.mousePosition;
                actualPointing = hit.transform;
                scrollingStarted = true;
            }
            else if (hit.transform != null && hit.transform.GetComponent<ClickableButton>() != null)
            {
                if (actualPointing != null && actualPointing != hit.transform && actualPointing.GetComponent<ClickableButton>() != null)
                    actualPointing.GetComponent<ClickableButton>().pointing = false;

                actualPointing = hit.transform;
                actualPointing.GetComponent<ClickableButton>().pointing = true;

                if (actualPointing.GetComponent<IClickable>() != null && pressedDown)
                {
                    actualPointing.GetComponent<IClickable>().Clicked();
                }
            }
            else if (hit.transform == null)
            {
                if (actualPointing != null && actualPointing.GetComponent<ClickableButton>() != null)
                {
                    actualPointing.GetComponent<ClickableButton>().pointing = false;
                    actualPointing = null;
                }
            }

        }
        else if (scrollingStarted && pressedDown && actualPointing != null)
        {
            prevoiusPoint = actualPoint;
            actualPoint = Input.mousePosition;
            Vector3 change = prevoiusPoint- actualPoint;

            actualPointing.GetComponent<IScrollable>().GetControllerChange(change);

        }
        else if (scrollingStarted && !pressedDown)
        {
            scrollingStarted = false;
            actualPointing = null;
        }

          
        
       

    }
}
