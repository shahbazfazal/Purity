using UnityEngine;
using System.Collections;

public class LiquidFire : MonoBehaviour 
{
	GameManager gm;

	void Start()
	{
		gm = GameObject.FindObjectOfType<GameManager> ();
	}
	void Update ()
	{
		
		StartCoroutine ("RotateLF");
	}

	IEnumerator RotateLF()
	{
		yield return new WaitForSeconds (1);
		transform.Rotate (-0.5f, 0.0f, 0.0f);

		StartCoroutine ("KillScript");
	}


	IEnumerator KillScript()
	{
		yield return new WaitForSeconds (1);
		gameObject.tag = "LFNewPos";
		Destroy (this);
	}

	public void Pour()
	{
		transform.Rotate (-1, 0, 0);
	}
}
	
