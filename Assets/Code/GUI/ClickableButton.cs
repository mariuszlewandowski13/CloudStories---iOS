using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableButton : MonoBehaviour {

    private bool _interactable;
    public bool interactable
    {
        get {
            return _interactable;
            }
        set {
            _interactable = value;
            
            ChangeButtonprojection();
        }
    }

    private bool _pointing;
    public bool pointing
    {
        get
        {
            return _pointing;
        }
        set
        {
            _pointing = value;
            ChangeButtonprojection();
        }
    }


    private Color disabledColor = new Color(0.314f, 0.314f, 0.314f, 1.0f);
    private Color normalColor = new Color(0.435f, 0.435f, 0.435f, 1.0f);
    private Color pointingColor = new Color(0.2f, 0.2f, 0.2f, 0.0f);

    public bool canChangeColors;

    private void ChangeButtonprojection()
    {
        if (canChangeColors)
        {
            if (!_interactable) GetComponent<Renderer>().material.color = disabledColor;
            else
            {
                if (pointing) GetComponent<Renderer>().material.color = pointingColor;
                else GetComponent<Renderer>().material.color = normalColor;
            }
        }
    }
}
