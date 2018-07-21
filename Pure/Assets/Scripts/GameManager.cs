using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    
    //CAMERAS
    public Camera[] cameras;
    public int currentCameraIndex;
    public bool ChangeToCam7;
    public bool ChangeToCam8;
    //CAMERAS

    public GameObject selectedObject;
    public GameObject lastSelectedObject;

    public GameObject Battery1;
    public GameObject Battery2;
    public GameObject Battery3;
    public GameObject Lith1;
    public GameObject Lith2;
    public GameObject Lith3;

    public GameObject GO_Tube;
    public bool MoveTube;
    public Vector3 MixingTubePos;
    public Vector3 IdleTubePos;

    public Vector3 MoveBatteryToCut;
    public GameObject Inventory;
    public List<GameObject> LitList = new List<GameObject>();
    public GameObject GO_Sparks;

    public GameObject LFCap;
    public GameObject LiquidFire;
    public GameObject BottleCap;


    public Obi.ObiEmitter LF_Emitter;

    public Transform PowderInstatiate;   
    public int HowManyPillsAdded = 0;
    public GameObject GO_Pestle;
    public GameObject GO_Powder;
    public GameObject GO_Mortar;

    public Transform InstanPowder;
    public GameObject GO_PseudoPowder;
    public int PowdersInMortar;

    public Transform PS_BB_Loc;
    public GameObject PS_BB;
//    public Obi.ObiEmitter PS_Emitter;
    public Transform PS_SB_Loc;
    public GameObject PS_SB;
//    public Obi.ObiEmitter PS_Emitter2;
    public int HowMuchPseudoIsLeft;
    public GameObject GO_Spoon;
    public Transform SpoonInstanLoc;
    private GameObject InstanSpoon;
    public Transform LittleSpoonLoc;
    public GameObject GO_LittleSpoon;
    //instantiated spoonn
    GameObject LittleSpoon;



    public bool HasLFBeenPoured;
    public bool HasLithBeenAdded;
    public bool HasPseudoBeenAdded;
    public bool HasLyeBeenAdded;
    public bool HasANBeenAdded;
    public bool HasWaterBeenAdded;
    public bool HasPseudoBeenAddedLittleBox;
    public bool CueSmoke;

    public Transform smokeInstanPoint;
    public GameObject GO_Smoke;

    public GameObject GO_Filter;

    public GameObject Lye_BB;
    public Transform Lye_BB_Loc;
    public GameObject GO_LyeSpoon;
//    public Obi.ObiEmitter LyeEmitter;

    public List<GameObject> Methamphetamine = new List<GameObject>();
    private int MethNumber = 0;

    public GameObject MC;
    public GameObject MCOptions;
    public GameObject Nitrate;
    private Vector3 temp;
    public Vector3 NitratePosHitTrigger;
//    public Obi.ObiEmitter ANEmitter;
    public GameObject AN_BB;
    public Transform AN_BB_Loc;

    public GameObject Rtube;
    public GameObject TapButton;
    public Obi.ObiEmitter WaterEmitter;
    public bool TubeInPosition;
    public Transform WaterNewPos;

    //AMOUNTS
    public int amount_LF = 0;
    public int amount_Pseudo1 = 0;
    public int amount_Pseudo2 = 0;
    public int amount_Lye = 0;
    public int amount_AN = 0;
    public int amount_Lith = 0;

    public int methPuritySum;
    //AMOUNTS

    public GameObject GO_Slider;
    public GameObject GO_SliderButton;
    private bool RandomNumberGeneration;
    public Text TE_PurityNumber;
    public GameObject MainMeth;
    public GameObject LastMeth;

    public GameObject Level2;

    public GameObject TouchDisable;


    void Start()
    {
        EnableTouch();
        RandomNumberGeneration = false;
//        FinalPurityLevel = 0;
        methPuritySum = 0;
        HasPseudoBeenAddedLittleBox = false;
        HasWaterBeenAdded = false;
        ChangeToCam7 = false;
        ChangeToCam8 = false;
        TubeInPosition = false;
        HasANBeenAdded = false;
        NitratePosHitTrigger = new Vector3();
        CueSmoke = false;
        HasPseudoBeenAdded = false;
        HasLithBeenAdded = false;
        HasLFBeenPoured = false;
        PowdersInMortar = 0;
        MoveTube = false;
        HasLyeBeenAdded = false;

        currentCameraIndex = 0;
        /*

        //Turn all cameras off, except the first default one
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        //If any cameras were added to the controller, enable the first one
        if (cameras.Length > 0)
        {
            cameras[0].gameObject.SetActive(true);
            Debug.Log("Camera with name: " + cameras[0].GetComponent<Camera>().name + ", is now enabled");
        }
        */
    }
    void Update()
    {
        
        PurityLevel();
        CheckMeth();

        if (RandomNumberGeneration == true)
        {
            TE_PurityNumber.text = Random.Range(0.0f, 100.0f).ToString();
        }

        CameraSelection();

		//FOR DEBUG
        if (Input.GetKeyDown(KeyCode.S))
        {
            CueSmoke = true;
        }

//        if (selectedObject.gameObject.tag == "SmallPot")
//        {
//            CheckMeth();
//        }


        if (ChangeToCam7 == true)
        {
            currentCameraIndex = 6;
            
            for (int i = 0; i < 6; i++)
            {
                cameras[i].gameObject.SetActive(false);
            }
            cameras[6].gameObject.SetActive(true);

            //ChangeToCam7 = false;
        }

        if (ChangeToCam8 == true)
        {
            currentCameraIndex = 7;

            for (int i = 0; i < 7; i++)
            {
                cameras[i].gameObject.SetActive(false);
            }
            cameras[7].gameObject.SetActive(true);

            ChangeToCam8 = false;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveTube = !MoveTube;
        }

        MovingTheTube();
        InstantiateSmoke();

        if (CueSmoke == true)
        {
            StartSmoke();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject hitObject = hitInfo.transform.parent.gameObject;

                lastSelectedObject = selectedObject;
                SelectObject(hitObject);
                MainBattery();
                MainBattery2();
                MainBattery3();
                RemoveTop();
                PourLF();
                AddingPills();
                Grind();
                AddPseudoToTable();
                //AddPseudo();
                //AddPowderToTable();
                Sparks();
                TransferPseudoUsingEmitter();
                UseFilter();
//                RemoveFilter();
                AddLye();
                MoveMCtoChooseML();
                MoveRTubeToBox();
                StartWater();
                LittleSpoonToBox();
                SliderButtonActivate();
                
                //                MoveRTubeToFloor();

            }
            else
            {
                ClearSelection();
            }
        }

    }

	public void ResetCam()
	{
		currentCameraIndex = 0;

		for (int i = 0; i < 7; i++)
		{
			cameras[i].gameObject.SetActive(false);
		}
		cameras[0].gameObject.SetActive(true);

		//reset position of camera
		cameras[0].transform.position = (new Vector3 (3.0f, 1.0f, -0.0f));

		//reset rotation of camera
		cameras[0].transform.localEulerAngles = new Vector3(10.0f ,-90.0f ,0.0f);

		//reset FOV of camera
		cameras [0].fieldOfView = 60;


	}

	public void Tube()
	{
		MoveTube = !MoveTube;
	}

    public void DisableTouch()
    {
        TouchDisable.SetActive(true);
    }
    public void EnableTouch()
    {
        TouchDisable.SetActive(false);
    }

    void CameraSelection()
    {
        //If the c button is pressed, switch to the next camera
        //Set the camera at the current index to inactive, and set the next one in the array to active
        //When we reach the end of the camera array, move back to the beginning oF the array.
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentCameraIndex++;
            Debug.Log("C button has been pressed. Switching to the next camera");
            if (currentCameraIndex < cameras.Length)
            {
                cameras[currentCameraIndex - 1].gameObject.SetActive(false);
                cameras[currentCameraIndex].gameObject.SetActive(true);
                Debug.Log("Camera with name: " + cameras[currentCameraIndex].GetComponent<Camera>().name + ", is now enabled");
            }
            else
            {
                cameras[currentCameraIndex - 1].gameObject.SetActive(false);
                currentCameraIndex = 0;
                cameras[currentCameraIndex].gameObject.SetActive(true);
                Debug.Log("Camera with name: " + cameras[currentCameraIndex].GetComponent<Camera>().name + ", is now enabled");
            }
        }
    }

    public void CameraButton()
    {
        currentCameraIndex++;
        if (currentCameraIndex < cameras.Length)
        {
            cameras[currentCameraIndex - 1].gameObject.SetActive(false);
            cameras[currentCameraIndex].gameObject.SetActive(true);
        }
        else
        {
            cameras[currentCameraIndex - 1].gameObject.SetActive(false);
            currentCameraIndex = 0;
            cameras[currentCameraIndex].gameObject.SetActive(true);
        }
    }

    void PurityLevel()
    {
        methPuritySum = amount_AN + amount_LF + amount_Lith + amount_Lye
            + amount_Pseudo1 + amount_Pseudo2;
    }

    public void SelectObject(GameObject obj)
    {
        if (selectedObject != null)
        {
            if (obj == selectedObject)
                return;

            ClearSelection();
        }

        selectedObject = obj;
    }


    void ClearSelection()
    {
        selectedObject = null;
    }




    void MovingTheTube()
    {
        if (MoveTube == true)
            GO_Tube.transform.position = MixingTubePos;
        else
            GO_Tube.transform.position = IdleTubePos;
    }


    void MainBattery()
    {
        if (selectedObject.gameObject.name == "Battery")
        {
            //CutTheBatt ();
            BatteryCutting();
            return;
        }
        if (selectedObject.gameObject.tag == "Lith1")
        {
            ExtractLithium();
        }
    }

    void MainBattery2()
    {
        if (selectedObject.gameObject.name == "Battery (1)")
        {
            //CutTheBatt ();
            BatteryCutting2();
            return;
        }
        if (selectedObject.gameObject.tag == "Lith2")
        {
            ExtractLithium2();
        }
    }

    void MainBattery3()
    {
        if (selectedObject.gameObject.name == "Battery (2)")
        {
            //CutTheBatt ();
            BatteryCutting3();
            return;
        }
        if (selectedObject.gameObject.tag == "Lith3")
        {
            ExtractLithium3();
        }
    }

    void BatteryCutting()
    {
        //move to vector3 pos
        //rotate the object once
        //add rotation script

        //selectedObject.transform.position = MoveBatteryToCut;
        

        //start movetocut animation
        if (selectedObject.gameObject.name == "Battery")
        {
            DisableTouch();
            Battery1.GetComponent<Animation>().Play("BatteryStart");
            StartCoroutine("Battery1StartAnim");
            return;
        }
    }

    void BatteryCutting2()
    {
        if (selectedObject.gameObject.name == "Battery (1)")
        {
            DisableTouch();
            Battery2.GetComponent<Animation>().Play("Battery2Start");
            StartCoroutine("Battery2StartAnim");
            return;
        }
    }

    void BatteryCutting3()
    {
        if (selectedObject.gameObject.name == "Battery (2)")
        {
            DisableTouch();
            Battery3.GetComponent<Animation>().Play("Battery3Start");
            StartCoroutine("Battery3StartAnim");
            return;
        }
    }

    IEnumerator Battery1StartAnim()
    {
        yield return new WaitForSeconds(1);
        Battery1.transform.Rotate(0, -90, 0);
        Battery1.gameObject.AddComponent<Rotation>();
    }

    IEnumerator Battery2StartAnim()
    {
        yield return new WaitForSeconds(1);
        Battery2.transform.Rotate(0, -90, 0);
        Battery2.gameObject.AddComponent<Rotation2>();
    }

    IEnumerator Battery3StartAnim()
    {
        yield return new WaitForSeconds(1);
        Battery3.transform.Rotate(0, -90, 0);
        Battery3.gameObject.AddComponent<Rotation3>();
    }

    public void CutTheBatt()
    {
        //Debug.Log ("BATTERY");
        //move battery forward
        //selectedObject.transform.position += new Vector3(0.1f, 0.015f, 0f);
        //		selectedObject.transform.Rotate (0, 90, 0);

        //set selectedobject to BatteryTop(child 2)
        selectedObject = selectedObject.transform.GetChild(1).gameObject;
        //move BatteryTop forward to reveal lithium
        selectedObject.transform.position += new Vector3(0.1f, 0f, 0f);

        //set selectedObject back to the parent
        selectedObject = selectedObject.transform.parent.gameObject;
        selectedObject.gameObject.tag = "BatteryCut";
        //		ClearSelection ();
    }

    void ExtractLithium()
    {
        GameObject Tempgo = selectedObject;
        DisableTouch();
        //select lithium
//        selectedObject = selectedObject.transform.GetChild(2).gameObject;

        //move Lithium forward
//       selectedObject.transform.position += new Vector3(0.08f, 0f, 0f);

        //set parent of lithium to empty
        Tempgo.transform.parent.gameObject.tag = "Empty";
    
        //move lithum to child of inventory and change the tag
        Tempgo.transform.parent = Inventory.transform;
        //selectedObject.transform.tag = "Lithium";

       //StartCoroutine("DestroyEmpty");
        StartCoroutine("AddToInventory");

    }

    void ExtractLithium2()
    {
        GameObject Tempgo = selectedObject;
        DisableTouch();
        
        Tempgo.transform.parent.gameObject.tag = "Empty";

        Tempgo.transform.parent = Inventory.transform;

        //StartCoroutine("DestroyEmpty2");
        StartCoroutine("AddToInventory2");
    }

    void ExtractLithium3()
    {
        GameObject Tempgo = selectedObject;
        DisableTouch();

        Tempgo.transform.parent.gameObject.tag = "Empty";

        Tempgo.transform.parent = Inventory.transform;

        //StartCoroutine("DestroyEmpty3");
        StartCoroutine("AddToInventory3");
    }

    IEnumerator DestroyEmpty()
    {
        yield return new WaitForSeconds(1);

        Destroy(GameObject.FindGameObjectWithTag("Empty"));
    }

    IEnumerator DestroyEmpty2()
    {
        yield return new WaitForSeconds(1);

        Destroy(GameObject.FindGameObjectWithTag("Empty"));
    }

    IEnumerator AddToInventory()
    {
        yield return new WaitForSeconds(1);

        Lith1.GetComponent<Animation>().Play();

        Lith1.transform.tag = "LithAdded";

        HasLithBeenAdded = true;

        amount_Lith += (Random.Range(1,4));

        //add to list when Lithium is inside of the box
        LitList.Add(Lith1);

        EnableTouch();

        //
        //		LitList.Add (GameObject.FindGameObjectWithTag ("Lithium"));
        //
        //		LitList = GameObject.FindGameObjectsWithTag ("Lithium");
        //
        //		GameObject.FindGameObjectWithTag ("Lithium").SetActive (false);
        //
        //		//count how many lithium in inventory
        //		Debug.Log (LitList.Count);
    }

    IEnumerator AddToInventory2()
    {
        yield return new WaitForSeconds(1);

        Lith2.GetComponent<Animation>().Play("AddLith2");

        Lith2.transform.tag = "LithAdded";

        HasLithBeenAdded = true;

        amount_Lith += (Random.Range(1, 4));

        LitList.Add(Lith2);

        EnableTouch();
    }

    IEnumerator AddToInventory3()
    {
        yield return new WaitForSeconds(1);

        Lith3.GetComponent<Animation>().Play("AddLith3");

        Lith3.transform.tag = "LithAdded";

        HasLithBeenAdded = true;

        amount_Lith += (Random.Range(1, 4));

        LitList.Add(Lith3);

        EnableTouch();
    }

    void Sparks()
    {
        if (LiquidFire.gameObject.tag == "LFPourStart")
        {
            StartCoroutine("InstantiateSparks");
        }
    }

    IEnumerator InstantiateSparks()
    {
        yield return new WaitForSeconds(2);

        for (int i = 0; i < LitList.Count; i++)
        {
            Instantiate(GO_Sparks, LitList[i].transform.position, transform.rotation);
        }

    }

    //-----------------------LIQUID FIRE-----------------------

    public void RemoveTop()
    {


        if (selectedObject.gameObject.tag == ("LiquidFire"))
        {
            selectedObject.gameObject.tag = "LFNoTop";
            //selectedObject is the Cap of the bottle
            selectedObject = selectedObject.transform.GetChild(1).gameObject;
            selectedObject.transform.position += new Vector3(0.8f, -0.3f, 0.8f);
            selectedObject.transform.parent = LFCap.transform;
        }

        if (selectedObject.gameObject.tag == ("LFNoTop"))
        {
            LiquidFire.GetComponent<Animation>().Play("LF_HighPour");
            LiquidFire.AddComponent<LiquidFire>();

            //TAG HAS BEEN CHANGE TO LFNewPos FROM LIQUIDFIRE SCRIPT
        }
    }

    void PourLF()
    {

        if (selectedObject.gameObject.tag == ("LFNewPos"))
        {
            selectedObject.gameObject.tag = "LFPourStart";
            selectedObject.gameObject.transform.Rotate(-60, 0, 0);

            HasLFBeenPoured = true;
            
            //INSTANTIATE DROPS OF LIQUID HERE
            LF_Emitter.speed = 2f;

            StartCoroutine("FinishPour");
        }
    }

    IEnumerator FinishPour()
    {
        Vector3 LF_Position = new Vector3(-0.6f, 0.0f, -1.0f);

        yield return new WaitForSeconds(2);

        if (selectedObject.gameObject.tag == ("LFPourStart"))
        {
            LF_Emitter.speed = 0f;
            LiquidFire.GetComponent<Animation>().Play("Anim_FinishPour");
            //			Destroy (selectedObject.gameObject);
            //			Destroy (GameObject.Find ("Cap"));
            //			Instantiate (InstanLF, InstanLFpos.position, InstanLFpos.rotation);
            //			InstanLF.gameObject.tag = "LiquidFire";

            //			selectedObject.gameObject.transform.Rotate (60, 0, 0);
            LiquidFire.gameObject.tag = "LiquidFire";
            //			LiquidFire.gameObject.transform.position = LF_Position;
            //			selectedObject.gameObject.transform.Rotate (120.005f, -0.561f, 0.0f);

            StartCoroutine("ApplyTop");
        }
    }

    IEnumerator ApplyTop()
    {
        yield return new WaitForSeconds(2);
        BottleCap.transform.parent = LiquidFire.transform;
        BottleCap.transform.localPosition = new Vector3(0f, 0f, 0f);
        amount_LF += (Random.Range(1, 4));


    }
    //-----------------------LIQUID FIRE-----------------------

    //-----------------------PSEUDO-----------------------

    void AddingPills()
    {
        if (selectedObject.gameObject.tag == ("PseudoPill"))
        {
            ///ani to send to pot
            selectedObject.gameObject.GetComponent<Animation>().Play();

            HowManyPillsAdded += 1;

            selectedObject.tag = "PillToPowder";

//            StartCoroutine("StartToGrind");
        }
    }

    void Grind()
    {
        if (HowManyPillsAdded == 3)
        {
            GO_Pestle.gameObject.GetComponent<Animation>().Play();
            HowManyPillsAdded = 0;
            StartCoroutine("MakePseudo");
        }
    }

    IEnumerator MakePseudo()
    {
        yield return new WaitForSeconds(3);

        GameObject Temp_powder = Instantiate(GO_Powder,
            PowderInstatiate.transform.position,
            PowderInstatiate.gameObject.transform.rotation);
        Temp_powder.transform.localScale += new Vector3(0.80f, 0.8f, 0.8f);

        Temp_powder.transform.parent = GO_Mortar.transform;

        Destroy(GameObject.Find("Pills"));

    }

    void AddPseudoToTable()
    {
        GameObject Pseudo_Powder;

        if (selectedObject.gameObject.tag == "PowderInMortar")
        {
            Destroy (GameObject.Find("PowderPos(Clone)"));
            Pseudo_Powder = Instantiate(GO_PseudoPowder, InstanPowder.position, InstanPowder.rotation);
            InstanSpoon = Instantiate(GO_Spoon, SpoonInstanLoc.position, SpoonInstanLoc.rotation);
            LittleSpoon = Instantiate(GO_LittleSpoon, LittleSpoonLoc.position, LittleSpoonLoc.rotation);
        }
    }

    //old pseudo code

    void AddPseudo()
    {
        if (selectedObject.gameObject.tag == ("PseudoPill"))
        {
            selectedObject.gameObject.GetComponent<Animation>().Play();
            selectedObject.gameObject.tag = ("PillToPowder");

            StartCoroutine("StartToGrind");
        }
    }

    IEnumerator StartToGrind()
    {
        yield return new WaitForSeconds(1);
        GO_Pestle.gameObject.GetComponent<Animation>().Play();
        StartCoroutine("CreatePowder");

        //		GameObject TempPseudo;
        //		TempPseudo = selectedObject.gameObject;
    }

    IEnumerator CreatePowder()
    {
        yield return new WaitForSeconds(2);
        GameObject Temp_powder;

        Temp_powder = Instantiate(GO_Powder,
            selectedObject.gameObject.transform.GetChild(0).transform.position,
            selectedObject.gameObject.transform.rotation);
        Temp_powder.transform.parent = GO_Mortar.transform;
        Destroy(selectedObject);

        PowdersInMortar += 1;
    }

    void AddPowderToTable()
    {
        GameObject Pseudo_Powder;


        if (PowdersInMortar >= 3)
        {
            if (selectedObject.gameObject.tag == "PowderInMortar")
            {
                for (int i = 0; i < GO_Mortar.transform.childCount; i++)
                {
                    Destroy(GO_Mortar.transform.GetChild(i).gameObject);
                }
                Pseudo_Powder = Instantiate(GO_PseudoPowder, InstanPowder.position, InstanPowder.rotation);
                InstanSpoon = Instantiate(GO_Spoon, SpoonInstanLoc.position, SpoonInstanLoc.rotation);
                LittleSpoon = Instantiate(GO_LittleSpoon, LittleSpoonLoc.position, LittleSpoonLoc.rotation);

            }
        }
    }

    void TransferPseudoUsingEmitter()
    {
        if (selectedObject.gameObject.tag == ("SpoonStart"))
        {
            InstanSpoon.GetComponent<Animation>().Play("SpoonPour");
            HowMuchPseudoIsLeft = 3;
            //            HowMuchPseudoIsLeft -= 1;
            StartCoroutine("StartPseudoEmit");

            //            if (HowMuchPseudoIsLeft <= 0)
            //            {
            //                Destroy(GO_PseudoPowder);
            //            }
        }

    }

    void LittleSpoonToBox()
    {
        if (selectedObject.gameObject.tag == "LittleSpoonStart")
        {
            LittleSpoon.GetComponent<Animation>().Play("LittleSpoon");
            StartCoroutine("LittleSpoonStartPseudoEmit");
        }
    }
    IEnumerator LittleSpoonStartPseudoEmit()
    {
        yield return new WaitForSeconds(1);
        Instantiate(PS_SB, PS_SB_Loc.position, PS_SB_Loc.rotation);        //PS_Emitter2.speed = 1f;
        HasPseudoBeenAddedLittleBox = true;
        amount_Pseudo2 += (Random.Range(3, 8));
    }




    IEnumerator StartPseudoEmit()
    {
        yield return new WaitForSeconds(1f);
        //PS_Emitter.speed = 1f;
        //PS_BB.SetActive(true);
        Instantiate(PS_BB, PS_BB_Loc.position, PS_BB_Loc.rotation);
        HasPseudoBeenAdded = true;
        amount_Pseudo1 += (Random.Range(1, 4));
        StartCoroutine("StopPseudoEmit");

    }

    IEnumerator StopPseudoEmit()
    {
        yield return new WaitForSeconds(1f);
        InstanSpoon.GetComponent<Animation>().Play("SpoonEndAnim");


    }
    //-----------------------PSEUDO-----------------------

    //-----------------------FILTER-----------------------
    void UseFilter()
    {
        if (selectedObject.gameObject.tag == "FilterIdle")
        {
            GO_Filter.GetComponent<Animation>().Play("UseFilter");

            StartCoroutine("ChangeFilterTag");

        }
    }

    IEnumerator ChangeFilterTag()
    {
        yield return new WaitForSeconds(1);
        selectedObject.gameObject.tag = "FilterUsing";
    }

 //   void RemoveFilter()
 //   {
 //       if (selectedObject.gameObject.tag == "FilterUsing")
 //       {
 //           GO_Filter.GetComponent<Animation>().Play("RemoveFilter");
 //           selectedObject.gameObject.tag = "FilterIdle";
 //       }
 //   }
    //-----------------------FILTER-----------------------

    //-----------------------LYE-----------------------
    void AddLye()
    {
        if (selectedObject.gameObject.tag == "LyeIdle")
        {
            GO_LyeSpoon.GetComponent<Animation>().Play("SpoonAddLye");
            selectedObject.gameObject.tag = "LyeAdded";
            StartCoroutine("LyeAdd");
        }
    }

    IEnumerator LyeAdd()
    {
        yield return new WaitForSeconds(1);
        Instantiate(Lye_BB, Lye_BB_Loc.position, Lye_BB_Loc.rotation);
        //LyeEmitter.speed = 1f;
        StartCoroutine("LyeIdle");
        HasLyeBeenAdded = true;
        amount_Lye += (Random.Range(1, 4));
    }

    IEnumerator LyeIdle()
    {
        yield return new WaitForSeconds(1);
        //LyeEmitter.speed = 0f;
        GO_LyeSpoon.GetComponent<Animation>().Play("SpoonLyeIdle");
        selectedObject.gameObject.tag = "LyeIdle";
    }
    //-----------------------LYE-----------------------

    ///-----------------------SMOKE-----------------------
    void InstantiateSmoke()
    {
        if (HasLFBeenPoured == true && HasLithBeenAdded == true &&
            HasPseudoBeenAdded == true && HasLyeBeenAdded == true &&
            HasANBeenAdded == true && HasWaterBeenAdded == true)
        {
            CueSmoke = true;
            Debug.Log("Cue Smoke");
        }
    }

    void StartSmoke()
    {
        Instantiate(GO_Smoke, smokeInstanPoint.position,
            smokeInstanPoint.rotation);

        //turn off this function so smoke does not instantiate twice
        CueSmoke = false;

        //turn all ingrediants to false
        HasLFBeenPoured = false;
        HasLithBeenAdded = false;
        HasPseudoBeenAdded = false;
        HasLyeBeenAdded = false;
        HasANBeenAdded = false;
        HasWaterBeenAdded = false;
    }
    ///-----------------------SMOKE-----------------------

    //-----------------------METH-----------------------
    public void CreateMeth()
    {//if the number of meth created is less than 35... by default it is 0
        if (MethNumber <= 35)
        {
            Methamphetamine[MethNumber].SetActive(true);
            Methamphetamine[MethNumber].AddComponent<Rigidbody>();
            Methamphetamine[MethNumber].AddComponent<MethForce>();

			//increase methnumber by 1
            MethNumber++;
			FocusOnMeth ();
        }
        if (MethNumber >= 35)
        {
            //do nothing
        }

    }


	void FocusOnMeth()
	{
		currentCameraIndex = 1;

		for (int i = 0; i < 6; i++)
		{
			cameras[i].gameObject.SetActive(false);
		}
		cameras[1].gameObject.SetActive(true);
	}
	    //-----------------------METH-----------------------

    //-----------------------CUP-----------------------
    void MoveMCtoChooseML()
    {
        Vector3 newMCPosition = new Vector3(3.2985f, 2.154f, 1.8182f);

        if (selectedObject.gameObject.tag == "MCIdle")
        {
            MC.transform.position = newMCPosition;
            MCOptions.SetActive(true);

            //SET ROTATION
            MC.transform.rotation = Quaternion.Euler(0, 0, 0);

            //turn it on
            Nitrate.SetActive(true);

            //change to camera7
            ChangeToCam7 = true;
        }
    }

    //fill MC with AN
    public void ResizeNitrate200(float RSamount)
    {
        //set the rotation of nitrate to 0, 0, 0
        Nitrate.transform.rotation = Quaternion.Euler(0, 0, 0);
        //resize
        temp = Nitrate.transform.localScale;

        temp.y += RSamount;

        Nitrate.transform.localScale = temp;

        //change tag
        MC.gameObject.tag = "MCFilled";

        //set options to not active
        MCOptions.SetActive(false);

        Nitrate.AddComponent<Rigidbody>();

        //start animation
        MC.GetComponent<Animation>().Play("MCtoBoxNew");

        StartCoroutine("RotateMCFunction");

        amount_AN += 2;

    }
    public void ResizeNitrate400(float RSamount)
    {
        //set the rotation of nitrate to 0, 0, 0
        Nitrate.transform.rotation = Quaternion.Euler(0, 0, 0);

        //resize
        temp = Nitrate.transform.localScale;

        temp.y += RSamount;

        Nitrate.transform.localScale = temp;

        //change tag
        MC.gameObject.tag = "MCFilled";

        //set options to not active
        MCOptions.SetActive(false);

        Nitrate.AddComponent<Rigidbody>();

        //start animation
        MC.GetComponent<Animation>().Play("MCtoBoxNew");

        StartCoroutine("RotateMCFunction");

        amount_AN += 4;

    }
    public void ResizeNitrate600(float RSamount)
    {
        //set the rotation of nitrate to 0, 0, 0
        Nitrate.transform.rotation = Quaternion.Euler(0, 0, 0);

        //resize
        temp = Nitrate.transform.localScale;

        temp.y += RSamount;

        Nitrate.transform.localScale = temp;

        //change tag
        MC.gameObject.tag = "MCFilled";

        //set options to not active
        MCOptions.SetActive(false);

        Nitrate.AddComponent<Rigidbody>();

        //start animation
        MC.GetComponent<Animation>().Play("MCtoBoxNew");

        StartCoroutine("RotateMCFunction");

        amount_AN += 6;
    }
    public void ResizeNitrate800(float RSamount)
    {
        //set the rotation of nitrate to 0, 0, 0
        Nitrate.transform.rotation = Quaternion.Euler(0, 0, 0);

        //resize
        temp = Nitrate.transform.localScale;

        temp.y += RSamount;

        Nitrate.transform.localScale = temp;

        //change tag
        MC.gameObject.tag = "MCFilled";

        //set options to not active
        MCOptions.SetActive(false);

        Nitrate.AddComponent<Rigidbody>();

        //start animation
        MC.GetComponent<Animation>().Play("MCtoBoxNew");

        StartCoroutine("RotateMCFunction");

        amount_AN += 8;
    }
    public void ResizeNitrate1000(float RSamount)
    {

        //set the rotation of nitrate to 0, 0, 0
        Nitrate.transform.rotation = Quaternion.Euler(0, 0, 0);

        //resize
        temp = Nitrate.transform.localScale;

        temp.y += RSamount;

        Nitrate.transform.localScale = temp;

        //change tag
        MC.gameObject.tag = "MCFilled";

        //set options to not active
        MCOptions.SetActive(false);

        Nitrate.AddComponent<Rigidbody>();

        //start animation
        MC.GetComponent<Animation>().Play("MCtoBoxNew");

        StartCoroutine("RotateMCFunction");

        amount_AN += 10;

    }


    IEnumerator RotateMCFunction()
    {
        yield return new WaitForSeconds(1);
        //add rotation script
        MC.AddComponent<MCRotate>();
        cameras[6].gameObject.SetActive(false);
        cameras[0].gameObject.SetActive(true);
        currentCameraIndex = 0;
        ChangeToCam7 = false;

        //ROTATION IS CONTROLLED FROM MCROTATE SCRIPT

    }


    //-----------------------CUP-----------------------

    //-----------------------TUBE AND WATER-----------------------
    void MoveRTubeToBox()
    {
        if (selectedObject.gameObject.tag == "RTubeIdle")
        {
            Rtube.GetComponent<Animation>().Play("RTubeToBox");
            TubeInPosition = true;
            selectedObject.gameObject.tag = "RTubeInUse";
        }

    }


    void MoveRTubeToFloor()
    {
        if (selectedObject.gameObject.tag == "RTubeInUse")
        {
            StartCoroutine("RTubeToFloor");
        }


    }

    IEnumerator RTubeToFloor()
    {
        yield return new WaitForSeconds(1);
        Rtube.GetComponent<Animation>().Play("RTubeToFloor");
        Rtube.gameObject.tag = "RTubeIdle";
    }


    void StartWater()
    {
        if (selectedObject.gameObject.name == "ButtonPos")
        {
            //rescale the button
            TapButton.transform.localScale = new Vector3(0.05f, 0.01f, 0.05f);
            WaterEmitter.speed = 0.5f;
            StartCoroutine("ResetButton");

            if (TubeInPosition == true)
            {
                HasWaterBeenAdded = true;
                //Tube.transform.tag = "GlassFull";
                WaterEmitter.transform.position = WaterNewPos.transform.position;
            }
        }
    }

    IEnumerator ResetButton()
    {
        yield return new WaitForSeconds(2);
        TapButton.transform.localScale = new Vector3(0.1f, 0.05f, 0.1f);
        WaterEmitter.speed = 0;
    }

    //-----------------------TUBE AND WATER-----------------------

    //-----------------------SLIDER-----------------------
    void CheckMeth()
    {
        if (LastMeth.activeInHierarchy)
        {
            ChangeToCam8 = true;
            MainMeth.GetComponent<Animation>().Play("MethToPurifier");
            LastMeth.SetActive(false);
        }
    }

    void SliderButtonActivate()
    {
        if (selectedObject.gameObject.tag == "PurifyButton")
        {
            //rescale the button
            GO_SliderButton.transform.localScale = new Vector3(0.05f, 0.01f, 0.05f);
            SliderAnimation();
            StartCoroutine("ResetSliderButton");
            RandomNumberGeneration = true;
        }
    }

    IEnumerator ResetSliderButton()
    {
        yield return new WaitForSeconds(10);
        GO_SliderButton.transform.localScale = new Vector3(0.05f, 0.03f, 0.05f);
        RandomNumberGeneration = false;
        TE_PurityNumber.text = methPuritySum.ToString();
        PlayerPrefs.SetInt("FinalPurityLevel", methPuritySum);
        //sell meth
        //activate button to start level 2
        Level2.SetActive(true);
        
    }

    public void StartLevel2()
    {
        Application.LoadLevel("Level2");
    }

    void SliderAnimation()
    {
        //start animation
        GO_Slider.GetComponent<Animation>().Play("Slider");
        MainMeth.transform.parent = GO_Slider.transform;
    }
    //-----------------------SLIDER-----------------------

    void OnGUI()
    {
        //		GUI.Box(new Rect(20, 20, 300, 50), "This is a box");
    }

}