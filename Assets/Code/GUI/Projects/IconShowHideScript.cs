using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconShowHideScript : MonoBehaviour {

    public bool isActive = true;

    private Transform referenceObject;
	
	void Update () {
        if (referenceObject != null)
        {
            bool toActivate = CheckObjectProperDistance();
            Activation(toActivate);
        }

	}

    public void Activation(bool toActivate)
    {
        if (toActivate && !isActive)
        {
            ActivateObject();
        }
        else if (!toActivate && isActive)
        {
            DeactivateObject();
        }
    }

    public bool CheckObjectProperDistance()
    {
        if (Mathf.Abs(transform.position.y -referenceObject.transform.position.y) > referenceObject.transform.lossyScale.y / 2.0f)
        {
            return false;
        }
        else {
            return true;
        }
    }

    public void SetReferenceObject(Transform reference)
    {
        referenceObject = reference;
    }

    private void ActivateObject()
    {
        if (GetComponent<ClickableButton>() == null) GetComponent<ClickableButton>().interactable = true;
        if (GetComponent<Renderer>() != null) GetComponent<Renderer>().enabled = true;
        if (transform.Find("Text").GetComponent<Renderer>() != null) transform.Find("Text").GetComponent<Renderer>().enabled = true;
        isActive = true;
    }

    private void DeactivateObject()
    {
        if (GetComponent<ClickableButton>() != null) GetComponent<ClickableButton>().interactable = false;
        if (GetComponent<Renderer>() != null) GetComponent<Renderer>().enabled = false;
        if (transform.Find("Text").GetComponent<Renderer>() != null) transform.Find("Text").GetComponent<Renderer>().enabled = false;
        isActive = false;
    }
}
