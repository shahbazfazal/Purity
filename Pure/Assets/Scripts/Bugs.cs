using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bugs : MonoBehaviour {

    public GameManager GM = GameObject.Find("_GameManager").GetComponent<GameManager>();

	// Use this for initialization
	void Start ()
    {
        
    }
	
    
	// Update is called once per frame
	void Update ()
    {
        if (GM.selectedObject.tag == "Battery")
        {
            //deactivate selection until battery has been cut
            //activate collider infront of camera for a few seconds 
        }
	}
    
}
