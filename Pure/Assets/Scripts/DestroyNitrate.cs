using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNitrate : MonoBehaviour {
    
	GameManager gm;


	void Start()
	{

		gm = GameObject.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == ("LargeBottomTrigger"))
        {
			gm.HasANBeenAdded = true;

            //Remove rigiddbody on nitrate
            Destroy(gm.Nitrate.GetComponent<Rigidbody>());

            //set position of where the nitrate hit the BOX
            gm.NitratePosHitTrigger = gm.Nitrate.transform.position;

            //reset position and scale of nitrate
            gm.Nitrate.transform.localPosition = new Vector3(0f, 0f, 0f);
            gm.Nitrate.transform.localScale = new Vector3(1, 1, 1);

            //turn nitrate off
            gm.Nitrate.SetActive(false);

            //set MC back to idle tag
            //reset MC back to idle pos and idle rot
            gm.MC.tag = "MCIdle";
            gm.MC.transform.position = new Vector3(-1.213f, 1.115f, -0.877f);
            gm.MC.transform.rotation = Quaternion.Euler(173.848f, -1.5f, 5.7f);

            //EMITTER
            gm.AN_BB.transform.position = gm.NitratePosHitTrigger;
            Instantiate(gm.AN_BB, gm.AN_BB_Loc.position, gm.AN_BB_Loc.rotation);

        }
    }
}
