using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    public int Counter;
    GameManager gm;

    private void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        Counter = 0;
    }

    private void Update()
    {
        Debug.Log(Counter);
        MakeMeth();
    }

    void OnParticleCollision(GameObject other)
    {
        //Debug.Log("HIT");
        if (other.transform.name == ("Filter"))
        {
            Debug.Log("MAKE METH");
            Counter++;
        }
    }

    void MakeMeth()
    {
		if (Counter >= 10)
		{
			Counter = 0;
			//instantiate meth

			gm.CreateMeth();
		}
    }

}













