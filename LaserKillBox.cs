using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserKillBox : MonoBehaviour
{
    public GameObject deadScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Box Collider");
            if(!deadScreen.activeInHierarchy)
            {
                Time.timeScale = 0f;
                deadScreen.SetActive(true);
                GameManager.instance.PlayerDies();
                
            }
        }
    }
}
