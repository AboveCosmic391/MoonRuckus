using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperPickup : MonoBehaviour
{
    public string gunID;
    private bool isCollected;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isCollected)
        {
            //isCollected = true;
            PlayerController.instance.AddGun(gunID);
            Destroy(gameObject);
            isCollected = true;
        }
    }
}
