using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCRotate : MonoBehaviour 
{
    GameManager gm;

	void Start()
	{
        gm = FindObjectOfType<GameManager>();
	}

	void Update()
	{
		transform.Rotate(-1.5f, 0f, 0, 0);

		if (transform.localEulerAngles.x < 180)
		{
            //TO SET ROTATION OF MC
            //transform.rotation = Quaternion.Euler(0, 180, 0);

            //DEStroy script swhen x axis is less than 180 degrees
            Destroy(this);
		}


//		StartCoroutine("KillScript");
	}

	IEnumerator KillScript()
	{
		yield return new WaitForSeconds(2);

//		transform.Rotate(0, 180, 0);
         
		Destroy(this);
	}
}
