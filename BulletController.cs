using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float bulletSpeed;
    public float lifetime;
    public Rigidbody theRB;

    public GameObject impactEffect;

    public int damageValue = 1;

    public bool damageEnemy, damagePlayer;

    public GameObject headshot;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //moves the bullet in the direction it is facing multiplied by the bullet speed
        theRB.velocity = transform.forward * bulletSpeed;

        checkForBulletExpiry();
    }

    private void checkForBulletExpiry(){
        lifetime -= Time.deltaTime; // bullet will expire
        if(lifetime <= 0){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Enemy" && damageEnemy){
            //we struck an enemy with a bullet
            Debug.Log("Enemy body was hit");
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damageValue);
            PlayerPrefs.SetInt("hitsAchieved", PlayerPrefs.GetInt("hitsAchieved") + 1);
        }

        if(other.gameObject.tag == "Headshot"){
            Debug.Log("Headshot achieved");
            PlayerPrefs.SetInt("hitsAchieved", PlayerPrefs.GetInt("hitsAchieved") + 1);
            PlayerPrefs.SetInt("headshotAchieved", PlayerPrefs.GetInt("headshotAchieved") + 1);
            other.transform.parent.GetComponent<EnemyHealthController>().DamageEnemy(damageValue * 2);
        }
        
        if(other.gameObject.tag == "Player" && damagePlayer){
            //Debug.Log("Player has been hit at: " + transform.position);
            PlayerPrefs.SetInt("timesHit", PlayerPrefs.GetInt("timesHit") + 1);
            PlayerHealthController.instance.DamagePlayer(damageValue);
        }
        Destroy(gameObject);//destroy the bullet
        deployParticleBlast();
    }

    private void deployParticleBlast(){
        Instantiate(impactEffect, transform.position + (transform.forward * (-bulletSpeed * Time.deltaTime)), transform.rotation);
    }
}
