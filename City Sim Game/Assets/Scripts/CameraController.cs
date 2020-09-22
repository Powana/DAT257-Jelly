using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is made to controll camera functions
/*
 * Camera is the objcet of the main camera
 * targetZoom is the size of the camera
 * zoomFactor is to controll zoom strength
 * zoomlerpSpeed is to controll zoom speed
 */
public class CameraController : MonoBehaviour
{
	private Camera cam;
	private float targetZoom;
	private float zoomFactor = 3f;
	[SerializeField] private float zoomLerpSpeed = 10;

	// Start is called before the first frame update
	void Start()
	{
		cam = Camera.main;
		targetZoom = cam.orthographicSize;
	}

	// Update is called once per frame
	void Update()
	{
		float scrollData;
		scrollData = Input.GetAxis("Mouse ScrollWheel");
		targetZoom -= scrollData * zoomFactor;

		// The parameters specify the size bounds.
		targetZoom = Mathf.Clamp(targetZoom, 1f, 8f);
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
	}
}
