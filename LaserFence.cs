using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFence : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public LineRenderer laserLine;
    public float laserLength = 10f;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        laserLine.SetWidth(0.2f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(startPoint.position, endPoint.position - startPoint.position, out hit, laserLength, layerMask))
        {
            endPoint.position = hit.point;
        }
        laserLine.SetPosition(0, startPoint.position);
        laserLine.SetPosition(1, endPoint.position);
    }
}
