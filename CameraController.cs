using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController instance;

    public Transform target;
    private float startFOV;
    private float targetFOV;
    public float zoomSpeed = 5f;

    public Camera camera;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        startFOV = camera.fieldOfView;
        targetFOV = startFOV;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;

        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }

    public void ZoomIn(float zoomAmount)
    {
        targetFOV = zoomAmount;
    }
    

    public void ZoomOut()
    {
        targetFOV = startFOV;
    }
}
