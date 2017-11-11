using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThePoint : MonoBehaviour {

    public Transform destintaion; //you fetch it in advance
    public float sens = 10; //sensetivity
    
    // Update is called once per frame
    public void Update () {
        GetComponent<Rigidbody>().AddForce(destintaion.position - transform.position * sens);// = (destintaion.position - transform.position) * sens;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Selector>().Hoover <= 1.5f)
        {
            other.gameObject.GetComponent<Selector>().Hoover += .003f * Time.time;
            Handheld.Vibrate();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<Selector>().StartCoroutine("fadeOut");
    }
}
