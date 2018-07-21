using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour 
{
	//
	// VARIABLES
	//

	public float turnSpeed = 4.0f;		// Speed of camera turning when mouse moves in along an axis
	public float panSpeed = 0.1f;		// Speed of the camera when being panned
	public float zoomSpeed = 0.1f;		// Speed of the camera going back and forth

	private Vector3 mouseOrigin;	// Position of cursor when mouse dragging starts
	private bool isPanning;		// Is the camera being panned?
	private bool isRotating;	// Is the camera being rotated?
	private bool isZooming;		// Is the camera zooming?



	//
	// UPDATE
	//

	void Update () 
	{
		XClamps ();
		YClamps ();
		ZClamps ();

		// Get the left mouse button
		if(Input.GetMouseButtonDown(0))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isRotating = true;
		}

		// Get the right mouse button
		if(Input.GetMouseButtonDown(1))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isPanning = true;
		}

		// Get the middle mouse button
		if(Input.GetMouseButtonDown(2))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isZooming = true;
		}

		// Disable movements on button release
		if (!Input.GetMouseButton(0)) isRotating=false;
		if (!Input.GetMouseButton(1)) isPanning=false;
		if (!Input.GetMouseButton(2)) isZooming=false;

		// Rotate camera along X and Y axis
		if (isRotating)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

			transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
			transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
		}

		// Move the camera on it's XY plane
		if (isPanning)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

			Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);
			transform.Translate(move, Space.Self);
		}

		// Move the camera linearly along Z axis
		if (isZooming)
		{
			Camera.main.fieldOfView -= 1.0f; 

			if (Camera.main.fieldOfView <= 1.0f) 
			{
				Camera.main.fieldOfView = 60.0f;
			}

			//Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

			//Vector3 move = pos.y * zoomSpeed * transform.forward; 
			//transform.Translate(move, Space.World);
		}
	}

	void XClamps()
	{
//		Vector3 newXPos = Mathf.Clamp (transform.position.x, -5.0f, 6.0f);
//		Vector3 newYPos = Mathf.Clamp (new Vector3(transform.position.y, -1.0f, 4.0f));
//		Vector3 newZPos = new Vector3(transform.position.z, -8.0f, 6.0f);
		if (transform.position.x < -5.0f)
			transform.position = new Vector3 (-4.9f, transform.position.y, transform.position.z);
		if (transform.position.x > 6.0f)
			transform.position = new Vector3 (5.9f, transform.position.y, transform.position.z);
	}
	void YClamps()
	{
		//		Vector3 newXPos = Mathf.Clamp (transform.position.x, -5.0f, 7.0f);
		//		Vector3 newYPos = Mathf.Clamp (new Vector3(transform.position.y, -1.0f, 4.0f));
		//		Vector3 newZPos = new Vector3(transform.position.z, -8.0f, 6.0f);
		if (transform.position.y < -1.0f)
			transform.position = new Vector3 (transform.position.x, -0.9f, transform.position.z);
		if (transform.position.y > 4.0f)
			transform.position = new Vector3 (transform.position.x, 3.9f, transform.position.z);
	}
	void ZClamps()
	{
		//		Vector3 newXPos = Mathf.Clamp (transform.position.x, -5.0f, 7.0f);
		//		Vector3 newYPos = Mathf.Clamp (new Vector3(transform.position.y, -1.0f, 4.0f));
		//		Vector3 newZPos = new Vector3(transform.position.z, -8.0f, 6.0f);
		if (transform.position.z < -8.0f)
			transform.position = new Vector3 (transform.position.x, transform.position.y, -7.9f);
		if (transform.position.z > 6.0f)
			transform.position = new Vector3 (transform.position.x, transform.position.y, 5.9f);
	}

}