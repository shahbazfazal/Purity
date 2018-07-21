using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour 
{
    //ROTATION SCRIPT FOR THE BATTERY!!!

	GameManager gm;
	public Vector3 endPos; 

	void Start()
	{
		gm = GameObject.FindObjectOfType<GameManager> ();

		endPos = new Vector3 (-0.1412f, 0.015f, -1.3221f);
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
        gm.Battery1.GetComponent<Animation>().Play("BatteryEnd");
        //      transform.position = endPos;
        //		gm.CutTheBatt ();
        gm.Battery1.gameObject.tag = "BatteryDead";
        gm.Lith1.gameObject.tag = "Lith1";
        gm.EnableTouch();
		Destroy (this);
	}
}
