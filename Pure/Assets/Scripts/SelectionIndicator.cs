using UnityEngine;
using System.Collections;

public class SelectionIndicator : MonoBehaviour 
{
	GameManager mm;

	// Use this for initialization
	void Start () 
	{
		mm = GameObject.FindObjectOfType<GameManager> ();
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (mm.selectedObject != null) 
		{
			Bounds bigBounds = mm.selectedObject.GetComponentInChildren<Renderer> ().bounds;

			float diameter = bigBounds.size.z;
			diameter *= 2.0f;

			this.transform.position = new Vector3 (bigBounds.center.x, 0, bigBounds.center.z);
			this.transform.localScale = new Vector3 (diameter, 1, diameter);
		}
	
	}
}
