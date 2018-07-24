using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;
using Cinemachine;

public class Animations : MonoBehaviour {

    public Animator EveAnim;
    public GameObject Grenade;
    public Transform GrenadeLoc;
    public Transform RightHandPos;
    private Rigidbody RB;
    public CinemachineVirtualCamera vCM7;

    //public splineMove SM;

	// Use this for initialization
	void Start ()
    {
        EveAnim = GetComponent<Animator>();
        //SM = GameObject.FindObjectOfType<splineMove>();
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "JumpCol")
        {
            //EveAnim.SetBool("Jump", true);
            //StartCoroutine("JumpFalse");
            EveAnim.StopPlayback();
            EveAnim.Play("Jumping");
            //GetComponent<CharacterController>().enabled = false;

        }

        if (other.name == "RollCol")
        {
            EveAnim.StopPlayback();
            EveAnim.Play("Stand To Roll");
        }

        if (other.name == "JumpOverCol")
        {
            EveAnim.StopPlayback();
            EveAnim.Play("Jump Over");
        }

        if (other.name == "DropCol")
        {
            //EveAnim.SetBool("Drop", true);
            EveAnim.StopPlayback();
            EveAnim.Play("Standing To Crouched");
            //GetComponent<CharacterController>().enabled = false;
            StartCoroutine("GrenadeInstantiate");
        }
    }

    IEnumerator JumpFalse()
    {
        yield return new WaitForSeconds(1);
        EveAnim.SetBool("Jump", false);
    }

    IEnumerator GrenadeInstantiate()
    {
        yield return new WaitForSeconds(5);
        transform.Rotate(0, 55, 0);

        //instantiate grenade
        GameObject temp = Instantiate(Grenade, RightHandPos.position, RightHandPos.rotation);
        temp.transform.parent = RightHandPos;
        StartCoroutine("GrenadeThrow");
        vCM7.LookAt = temp.transform;
    }

    IEnumerator GrenadeThrow()
    {
        yield return new WaitForSeconds(3);

        RB = GameObject.Find("Grenade(Clone)").GetComponent<Rigidbody>();
        RB.useGravity = true;
        Debug.Log("THROW!");
        GameObject.Find("Grenade(Clone)").transform.parent = null;
        RB.AddForce(transform.forward * 600);

    }
}
