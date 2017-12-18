using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour, IScrollable {

    public bool canScrollUp;
    public bool canScrollDown;

    public Transform objectToScroll;

    private float scrollMultiplier = 0.05f;

    public void GetControllerChange(Vector3 change)
    {
        if ((canScrollUp && change.y > 0.0f) || (canScrollDown && change.y < 0.0f) )
        {
            Vector3 posChange = (new Vector3(0.0f, change.y, 0.0f))* scrollMultiplier;
            objectToScroll.position -= posChange;
        }
    }
}
