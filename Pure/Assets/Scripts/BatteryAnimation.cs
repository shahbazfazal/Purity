using UnityEngine;
using System.Collections;

public class BatteryAnimation : MonoBehaviour 
{
	GameManager gm;


	void Start()
	{

		gm = GameObject.FindObjectOfType<GameManager> ();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			if (gm.selectedObject.tag == "Battery")   
				CutTheBatt ();
			else if (gm.selectedObject.tag == "BatteryCut")
				ExtractLithium ();
		}

	}




	void CutTheBatt()
	{
		//Debug.Log ("BATTERY");
		//move battery forward
		gm.selectedObject.transform.position += new Vector3 (0.1f, 0.015f, 0f); 
		gm.selectedObject.transform.Rotate (0, 90, 0);

		//set selectedobject to BatteryTop(child 2)
		gm.selectedObject = gm.selectedObject.transform.GetChild (1).gameObject;
		//move BatteryTop forwars
		gm.selectedObject.transform.position += new Vector3 (0.1f, 0f, 0f);

		gameObject.tag = "BatteryCut";
		gm.selectedObject = null;
	}

	void ExtractLithium()
	{
		gm.selectedObject = gm.selectedObject.transform.GetChild (2).gameObject;

		//move Lithium forward
		gm.selectedObject.transform.position += new Vector3 (0.08f, 0f, 0f);

		gameObject.tag = "Empty";
		gm.selectedObject = null;
	}
}
