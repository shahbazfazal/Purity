using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneAnims : MonoBehaviour {

    private Animator EveAnim;

	// Use this for initialization
	void Start () {
        EveAnim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Climb")
        {
            //EveAnim.StopPlayback();
            EveAnim.Play("Sprint To Wall Climb");
        }

        if (other.name == "ClimbOver")
        {
            //EveAnim.StopPlayback();
            EveAnim.Play("Climbing");
        }
    }
}
