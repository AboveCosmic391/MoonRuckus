using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrigger : MonoBehaviour
{

    // BoxCollider boxCollider;
    // SphereCollider sphereCollider;
    
    public GameObject hintText;
    public bool canUnlock = false;
    public GameObject particleSystem1, particleSystem2, particleSystem3, particleSystem4, particleSystem5;
    public GameObject objectWithBoxCollider;
    public GameObject objectWithLineRenderer1, objectWithLineRenderer2, objectWithLineRenderer3, objectWithLineRenderer4, objectWithLineRenderer5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && canUnlock)
        {
            hintText.SetActive(false);
            canUnlock = false;
            particleSystem1.SetActive(false);
            particleSystem2.SetActive(false);
            particleSystem3.SetActive(false);
            particleSystem4.SetActive(false);
            particleSystem5.SetActive(false);
            if(objectWithBoxCollider.GetComponent<BoxCollider>() != null)
            {
                objectWithBoxCollider.GetComponent<BoxCollider>().enabled = false;
            }
            if(objectWithLineRenderer1.GetComponent<LineRenderer>() != null)
            {
                objectWithLineRenderer1.GetComponent<LineRenderer>().enabled = false;
            }
            if(objectWithLineRenderer2.GetComponent<LineRenderer>() != null)
            {
                objectWithLineRenderer2.GetComponent<LineRenderer>().enabled = false;
            }
            if(objectWithLineRenderer3.GetComponent<LineRenderer>() != null)
            {
                objectWithLineRenderer3.GetComponent<LineRenderer>().enabled = false;
            }
            if(objectWithLineRenderer4.GetComponent<LineRenderer>() != null)
            {
                objectWithLineRenderer4.GetComponent<LineRenderer>().enabled = false;
            }
            if(objectWithLineRenderer5.GetComponent<LineRenderer>() != null)
            {
                objectWithLineRenderer5.GetComponent<LineRenderer>().enabled = false;
            }
            // boxCollider.enabled = false;
            // GameManager.instance.UnlockCursor();
        }
    }



    // player has entered the sphere collider 
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Sphere Collider");
            if(!hintText.activeInHierarchy)
            {
                hintText.SetActive(true);
                canUnlock = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            hintText.SetActive(false);
            canUnlock = false;
        }
    }
}
