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
}
