using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparks : MonoBehaviour 
{
	GameManager gm;

	public int HowManyLithAdded;

	void Start()
	{
		gm = GameObject.FindObjectOfType<GameManager> ();

		HowManyLithAdded = 0;
	}
}
