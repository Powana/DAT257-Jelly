using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    private Camera cam;
    private float targetZoom;
    private float zoomFactor = 3f;
    [SeriallizeField] private float zoomLerpSpeed = 10;
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
        targetZoom = Mathf.Clamp(targetZoom, 4.5f, 8f);
        cam.orthographicSize = Matchf.Lerb(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
    }
}
