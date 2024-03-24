using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    public float moveSpeed;
    public float gravityModifier;
    public float runSpeed = 12f;

    //jumping
    public float jumpPower;
    private bool canJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;


    public CharacterController charController;

    //controlling direction of player motion in three directions
    private Vector3 moveInput;

    //reference to camera transform
    public Transform cameraTransform;

    public float mouseSensitivity;

    public bool invertX;
    public bool invertY;


    public Animator anim;


    //reference the the bullet
    public GameObject bullet;
    public Transform firePoint;

    // reference to the Gun object
    public Gun activeGun;
    public List<Gun> gunList = new List<Gun>();
    public List<Gun> unlockGuns = new List<Gun>();
    public int currentGunIndex;

    private int shotsFired = 0;


    // tracking shooting statistics
    private int hitsAchieved = 0;
    private int headshotAchieved = 0;
    private int timesHit = 0;
        


    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("List of guns: " + gunList.Count);
        currentGunIndex = 0;
        for(int i = 0; i < gunList.Count; i++)
        {
            Debug.Log("Gun: " + gunList[i].name);
            if( i == currentGunIndex)
            {
                gunList[i].gameObject.SetActive(true);
                activeGun = gunList[i];
            }
            else
            {
                gunList[i].gameObject.SetActive(false);
            }
        }
        PlayerPrefs.SetInt("shotsFired", shotsFired);
        
        PlayerPrefs.SetInt("hitsAchieved", hitsAchieved);
        PlayerPrefs.SetInt("headshotAchieved", headshotAchieved);
        PlayerPrefs.SetInt("timesHit", timesHit);

        // GameObject pistol = GameObject.Find("LaserPistol");
        // GameObject machineGun = GameObject.Find("Laser Repeater");
        // GameObject sniper = GameObject.Find("Laser Sniper");
    }



    // Update is called once per frame
    void Update()
    {

        // /*
        //OLD MOVEMENT SYSTEM
        // From Unity Input system
        // Horizontal is defaulted to 'a' and 'd' or 'left' and 'right' directional arrow
        // Left/Right
        // */
        // moveInput.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        // //back and forth motion
        // moveInput.z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        if(!UIHealthController.instance.pauseScreen.activeInHierarchy){
            
        
        // change guns
        if(Input.GetKeyDown(KeyCode.G))
        {
            SwitchWeapon();
        }


/*
Zoom in and out on the gun sights 
*/
        if(Input.GetMouseButtonDown(1))
        {
            CameraController.instance.ZoomIn(activeGun.zoomAmount);
        }

        if(Input.GetMouseButtonUp(1))
        {
            CameraController.instance.ZoomOut();
        }
        



        //store y velocity
        float yVel = moveInput.y;


        /*
        NEW MOVEMENT SYSTEM
        Player now moves in the direction they are "looking"
        */
        Vector3 verticalMove = transform.forward * Input.GetAxis("Vertical");
        Vector3 horizontalMove = transform.right * Input.GetAxis("Horizontal");

        // moveInput = verticalMove * moveSpeed * Time.deltaTime;
        moveInput = horizontalMove + verticalMove;
        moveInput.Normalize();

        if(Input.GetKey(KeyCode.LeftShift)){
            moveInput = moveInput * runSpeed;
        }else{
            moveInput= moveInput * moveSpeed;
        }
        


        moveInput.y = yVel;

        //deal with gravity
        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;
        if(charController.isGrounded){
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        //handle jumping - really critical for some reason to put after your physics
        //previously had it as the first lines of code in Update() method and did not work. nothing wrong with code; placement!

        canJump = Physics.OverlapSphere(groundCheckPoint.position, .25f, whatIsGround).Length > 0;

        if(Input.GetKeyDown(KeyCode.Space) && canJump){
            //moveInput.y = jumpPower;
            JumpPlayer();
        }



        //apply movement input to character controller
        charController.Move(moveInput * Time.deltaTime);


        /*
        control camera rotation
        */
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        if(invertX){
            mouseInput.x = -mouseInput.x;
        }
        if(invertY){
            mouseInput.y = -mouseInput.y;
        }
        
        //look left and right using mouse input
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        //look up and down based on mouse input [inverted motion - up motion on camera looks down]
        //cameraTransform.rotation = Quaternion.Euler(cameraTransform.rotation.eulerAngles + new Vector3(mouseInput.y, 0f, 0f));

        //look up and down based on mouse input [expected matching motion - up movement on mouse and player looks up]
        cameraTransform.rotation = Quaternion.Euler(cameraTransform.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));



        // fires single shot
        // prevents gun from firing any faster than the fire rate
        if(Input.GetMouseButtonDown(0) && activeGun.shotCounter <= 0){
            fireBullet();
        }

        // auto fire
        if(Input.GetMouseButton(0) && activeGun.canAutoFire){
            if(activeGun.shotCounter <= 0){
                fireBullet();
            }
        }



        anim.SetFloat("moveSpeed", moveInput.magnitude);
        }

    }

    private void fireBullet(){
        //crreate raycast hit object
        RaycastHit hit;
        if(Physics.Raycast(
            cameraTransform.position, //starting point
            cameraTransform.forward, //direction of raycast travel
            out hit, //if it hits something, store that information here
            50f //distance raycast travels
            )
        ){
            if(Vector3.Distance(cameraTransform.position, hit.point) > 2f){
                firePoint.LookAt(hit.point);
            }
            
        }else{
            firePoint.LookAt(cameraTransform.position+ (cameraTransform.forward * 30f)); 
        }
        // Instantiate(bullet, firePoint.position, firePoint.rotation);
        DischargeWeapon();
    }

    public void DischargeWeapon(){
        if(activeGun.currentAmmo > 0){
            activeGun.currentAmmo--;
            Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation);
            activeGun.shotCounter = activeGun.fireRate;
            activeGun.currentAmmoText.text = activeGun.currentAmmo.ToString() + " / ";
            SetWeaponAmmoAmountInPlayerPrefs();
        }
        
    }

    public void SetWeaponAmmoAmountInPlayerPrefs()
    {
            if(activeGun == gunList[0])
            {
                PlayerPrefs.SetInt("pistolAmmo", activeGun.currentAmmo);
            }
            if(activeGun == gunList[1])
            {
                PlayerPrefs.SetInt("machineGunAmmo", activeGun.currentAmmo);
            }
            if(activeGun == gunList[2])
            {
                PlayerPrefs.SetInt("sniperAmmo", activeGun.currentAmmo);
            }
        PlayerPrefs.SetInt("shotsFired", PlayerPrefs.GetInt("shotsFired") + 1);
    }

    public void CollectAmmo(int ammoAmount)
    {
        if(activeGun == gunList[0])
        {
            activeGun.maxAmmo = 25;
        }
        if(activeGun == gunList[1])
        {
            activeGun.maxAmmo = 100;
        }
        if(activeGun == gunList[2])
        {
            activeGun.maxAmmo = 10;
        }

        if(activeGun.currentAmmo >= activeGun.maxAmmo - ammoAmount){
            activeGun.currentAmmo = activeGun.maxAmmo;
        }else{
            activeGun.currentAmmo += ammoAmount;
        }
        activeGun.currentAmmoText.text = activeGun.currentAmmo.ToString() + " / ";
        SetWeaponAmmoAmountInPlayerPrefs();
    }

    void JumpPlayer(){
        Debug.Log("method called: player jumping");
        moveInput.y = jumpPower;
    }

    private void SwitchWeapon()
    {
        if(currentGunIndex < gunList.Count - 1)
        {
            gunList[currentGunIndex].gameObject.SetActive(false);
            currentGunIndex++;
            gunList[currentGunIndex].gameObject.SetActive(true);
            activeGun = gunList[currentGunIndex];
        }
        else
        {
            gunList[currentGunIndex].gameObject.SetActive(false);
            currentGunIndex = 0;
            gunList[currentGunIndex].gameObject.SetActive(true);
            activeGun = gunList[currentGunIndex];
        }
        firePoint.position = activeGun.firePoint.position;
        UpdateWeaponData();
    }

    private void UpdateWeaponData()
    {
        // for(int i = 0; i < gunList.Count; i++)
        // {
        //     switch(i)
        //     {
        //         case 0:
        //             PlayerPrefs.SetInt("pistolAmmo", gunList[i].currentAmmo);
        //             break;
        //         case 1:
        //             PlayerPrefs.SetInt("machineGunAmmo", gunList[i].currentAmmo);
        //             break;
        //         case 2:
        //             PlayerPrefs.SetInt("sniperAmmo", gunList[i].currentAmmo);
        //             break;
        //     }
        // }
        if(activeGun == gunList[0])
        {
            activeGun.currentAmmo = PlayerPrefs.GetInt("pistolAmmo");
            activeGun.currentAmmoText.text = activeGun.currentAmmo.ToString() + " / ";
            activeGun.maxAmmoText.text = "25";
            activeGun.equippedGunText.text = "Pistol";
        }
        if(activeGun == gunList[1])
        {
            activeGun.currentAmmo = PlayerPrefs.GetInt("machineGunAmmo");
            activeGun.currentAmmoText.text = activeGun.currentAmmo.ToString() + " / ";
            activeGun.maxAmmoText.text = "100";
            activeGun.equippedGunText.text = "Machine Gun";
        }
        if(activeGun == gunList[2])
        {
            activeGun.currentAmmo = PlayerPrefs.GetInt("sniperAmmo");
            activeGun.currentAmmoText.text = activeGun.currentAmmo.ToString() + " / ";
            activeGun.maxAmmoText.text = "10";
            activeGun.equippedGunText.text = "Sniper";
        }
    }

    public void AddGun(string gun)
    {
        Debug.Log("Gun collected: " + gun);
        // bool gununlocked = false;
        // if(unlockGuns.Count > 0)
        // {
        //     for(int i = 0; i < unlockGuns.Count; i++)
        //     {
        //         if(unlockGuns[i].name == gun)
        //         {
        //             gununlocked = true;
        //             gunList.Add(unlockGuns[i]);
        //             unlockGuns.RemoveAt(i);
        //             i = unlockGuns.Count;
        //         }
        //     }
        // }

        // if(gununlocked)
        // {
        //     currentGunIndex = gunList.Count - 2;
        // }
    }
}
