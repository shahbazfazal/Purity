using UnityEngine;
using System.Collections;

public class Rotation2 : MonoBehaviour 
{
    //ROTATION SCRIPT FOR THE BATTERY!!!

	GameManager gm;

	void Start()
	{
		gm = GameObject.FindObjectOfType<GameManager> ();
    }

	void Update()
	{
		transform.Rotate (0, 0, 5);
		StartCoroutine ("Pause5");
	}

	IEnumerator Pause5()
	{
		yield return new WaitForSeconds (5);
        transform.Rotate(0, 180, 0);
        gm.Battery2.GetComponent<Animation>().Play("Battery2End");
        //      transform.position = endPos;
        //		gm.CutTheBatt ();
        gm.Battery2.gameObject.tag = "BatteryDead";
        gm.EnableTouch();
		Destroy (this);
	}
}
