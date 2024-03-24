using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    // public Gun(GameObject _gunObject)
    // {
    //     this = _gunObject;
    // }

    public GameObject bullet;

    public bool canAutoFire;


    [HideInInspector]
    public float shotCounter;

    
    public int currentAmmo;
    public int maxAmmo;
    public float fireRate;
    public float zoomAmount;

    public Transform firePoint;


    // laser pistol
    public int currentPistolAmmo;
    public int maxPistolAmmo;
    private float fireRatePistol;
    


    // laser machine gun
    private int currentAmmoMachineGun;
    private int maxAmmoMachineGun;
    private float fireRateMachineGun;


    // laser sniper
    private int currentAmmoSniper;
    private int maxAmmoSniper;
    private float fireRateSniper;


    // UI text elements for ammo
    public Text currentAmmoText;
    public Text maxAmmoText;
    public Text equippedGunText;


    public LineRenderer lineRenderer;
    public float lineLength = 50f;
    public LayerMask layerMask;

    public string gunName;



    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = 10;
        currentAmmoText.text = currentAmmo.ToString() + " /";

        maxAmmo = 50;
        maxAmmoText.text = maxAmmo.ToString();

        equippedGunText.text = "Pistol";

        layerMask = ~LayerMask.GetMask("LaserPointer");
        // InitGunData();
    }

    // Update is called once per frame
    void Update()
    {
        if(shotCounter > 0)
        {
            shotCounter -= Time.deltaTime;
        } 

        Vector3 startPoint = firePoint.position;
        Vector3 endPoint = startPoint + firePoint.forward * lineLength;

        RaycastHit hit;
        if(Physics.Raycast(firePoint.position, firePoint.forward, out hit, lineLength, layerMask))
        {
            endPoint = hit.point;
        }

        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
        // InitGunData();
    }

    // public void InitGunData()
    // {
    //     // laser pistol
    //     currentPistolAmmo = 10;
    //     maxPistolAmmo = 50;
    //     fireRatePistol = 0.5f;

    //     // laser machine gun
    //     currentAmmoMachineGun = 30;
    //     maxAmmoMachineGun = 100;
    //     fireRateMachineGun = 0.2f;

    //     // laser sniper
    //     currentAmmoSniper = 5;
    //     maxAmmoSniper = 20;
    //     fireRateSniper = 1.5f;

    //     int x = PlayerController.instance.gunList.currentGunIndex;
    //     Debug.Log("Current gun index: " + x);
    //     if(x == 0)
    //     {
    //         currentAmmo = currentPistolAmmo;
    //         maxAmmo = maxPistolAmmo;
    //         fireRate = fireRatePistol;
    //     }
    //     else if(x == 1)
    //     {
    //         currentAmmo = currentAmmoMachineGun;
    //         maxAmmo = maxAmmoMachineGun;
    //         fireRate = fireRateMachineGun;
    //     }
    //     else if(x == 2)
    //     {
    //         currentAmmo = currentAmmoSniper;
    //         maxAmmo = maxAmmoSniper;
    //         fireRate = fireRateSniper;
    //     }
    //     ManageGunData();
    // }

    // public void ManageGunData()
    // {
    //     currentAmmoText.text = currentAmmo.ToString() + " /";
    //     maxAmmoText.text = maxAmmo.ToString();
    // }
}
