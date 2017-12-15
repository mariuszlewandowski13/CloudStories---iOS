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
        if (GetComponent<Button>() == null) GetComponent<Button>().interactable = true;
        if (GetComponent<Image>() != null) GetComponent<Image>().enabled = true;
        if (transform.Find("Text").GetComponent<Text>() != null) transform.Find("Text").GetComponent<Text>().enabled = true;
        isActive = true;
    }

    private void DeactivateObject()
    {
        if (GetComponent<Button>() != null) GetComponent<Button>().interactable = false;
        if (GetComponent<Image>() != null) GetComponent<Image>().enabled = false;
        if (transform.Find("Text").GetComponent<Text>() != null) transform.Find("Text").GetComponent<Text>().enabled = false;
        isActive = false;
    }
}
