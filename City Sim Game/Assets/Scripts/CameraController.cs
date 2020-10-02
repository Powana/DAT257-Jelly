using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

// This class is made to controll camera functions
/*
 * panSpeed is to controll how fast we move the camera 
 * panLimit is to controll the limit of x and y 
 * Camera is the objcet of the main camera
 * targetZoom is the size of the camera
 * zoomFactor is to controll zoom strength
 * zoomlerpSpeed is to controll zoom speed
 */
public class CameraController : MonoBehaviour
{
	public float panSpeed = 5f;
	public Vector2 panLimit;
    public float maxZoomOut = 10f;
    public Tilemap tilemapObject;

    private Camera cam;
	private float targetZoom;
	private float zoomFactor = 3f;
	[SerializeField] private float zoomLerpSpeed = 10;

	// Start is called before the first frame update
	void Start()
	{
		cam = GetComponent<Camera>();
		targetZoom = cam.orthographicSize;

        // Get center of tilemap and set camera to it
        Vector3 centerPos = tilemapObject.cellBounds.center;
        centerPos = tilemapObject.GetCellCenterWorld(new Vector3Int((int) centerPos.x, (int) centerPos.y, -1));
        cam.transform.position = centerPos;

	}

	// Update is called once per frame
	void Update()
	{
		ZoomFunction();
		CameraMovement();
	}




	//The method is responsible of Zoom in and zoom out feature
	// scrollData is to get the data of the mouse Scroll wheel
	void ZoomFunction()
    {

		float scrollData;
		scrollData = Input.GetAxis("Mouse ScrollWheel");
		targetZoom -= scrollData * zoomFactor;

		// The parameters specify the size bounds.
		targetZoom = Mathf.Clamp(targetZoom, 1f, maxZoomOut);
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
	}

	// The method is responsible of camera movement
	// pos is to get the position the z,x,y 
	void CameraMovement()
    {
		Vector3 pos = transform.position;

        if (Input.GetKey("w"))
        {
			pos.y += panSpeed * Time.deltaTime;
        }
		if (Input.GetKey("s"))
		{
			pos.y -= panSpeed * Time.deltaTime;
		}
		if (Input.GetKey("a"))
		{
			pos.x -= panSpeed * Time.deltaTime;
		}
		if (Input.GetKey("d"))
		{
			pos.x += panSpeed * Time.deltaTime;
		}

		pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);
		pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);


		transform.position = pos;

	}

}
