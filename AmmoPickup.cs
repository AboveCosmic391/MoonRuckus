using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoPickup : MonoBehaviour
{

    private bool isCollected = false;
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
        if(other.tag == "Player" && !isCollected)
        {
            isCollected = true;
            // if(PlayerController.instance.activeGun.currentAmmo >= PlayerController.instance.activeGun.maxAmmo - 10)
            // {
            //     PlayerController.instance.activeGun.currentAmmo = PlayerController.instance.activeGun.maxAmmo;
            // }else{
            //     PlayerController.instance.activeGun.currentAmmo += 10;
            // }
            PlayerController playerController = other.GetComponent<PlayerController>();
            if(playerController != null)
            {
                playerController.CollectAmmo(10);
            }
            UpdateAmmoState();
            
        }
    }

    private void UpdateAmmoState()
    {
        PlayerController.instance.activeGun.currentAmmoText.text = PlayerController.instance.activeGun.currentAmmo.ToString() + " /";
        Destroy(gameObject);
    }
}
