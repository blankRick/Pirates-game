using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AImove : MonoBehaviour {
    
    void Start () {
        GetComponent<Rigidbody>().freezeRotation = true;
    }
    

    
}
