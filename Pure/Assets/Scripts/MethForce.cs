using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MethForce : MonoBehaviour
{
    Rigidbody rb;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Random.Range(-10, 10), 
            Random.Range(-10, 10), 
            Random.Range(-10, 10));
    }
	
	// Update is called once per frame
	void Update ()
    {
	    	
	}
}
