using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{


    private bool chasing;
    public float distanceToChase = 10f;
    public float distanceToLose = 15f;
    public float distanceToStop = 2f;

    float difference;
    private Vector3 targetPoint;
    

    public NavMeshAgent agent; 
    public float keepChasingTime = 5f;
    private float chaseCounter; 

    private Vector3 startingPoint; 

    public GameObject bullet;
    public Transform firePoint;
    public float fireRate, waitBetweenShots = 2f, timeToShoot = 1f; 
    private float fireCount, shotWaitCounter, shootTimeCounter;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        startingPoint = transform.position;
        shootTimeCounter = timeToShoot;
        shotWaitCounter = waitBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {

        targetPoint = PlayerController.instance.transform.position;
        targetPoint.y = transform.position.y;

        // difference = transform.position.x - PlayerController.instance.transform.position.x;
        // Debug.Log("distance from enemy: " + difference);

        if(!chasing){

            if(Vector3.Distance(transform.position, targetPoint) < distanceToChase){
                chasing = true;
                //fireCount = 1f;
                shootTimeCounter = timeToShoot;
                shotWaitCounter = waitBetweenShots;
            }

            if(chaseCounter > 0){

                chaseCounter -= Time.deltaTime;

                if(chaseCounter <= 0){
                    agent.destination = startingPoint;
                }

            }

            if(agent.remainingDistance < 0.25f){
                anim.SetBool("isMoving", false);
            }
            
        }else{


            // transform.LookAt(PlayerController.instance.transform.position);
            // rb.velocity = transform.forward * moveSpeed;

            if(Vector3.Distance(transform.position, targetPoint) > distanceToStop){
                agent.destination = targetPoint;
            }else{
                agent.destination = transform.position;
            }

            if(Vector3.Distance(transform.position, targetPoint) > distanceToLose){
                chasing = false;

                //enemy has "lost" the player
                chaseCounter = keepChasingTime;
            }

            if(shotWaitCounter > 0){
                shotWaitCounter -= Time.deltaTime;

                if(shotWaitCounter <= 0){
                    shootTimeCounter = timeToShoot;
                }

                anim.SetBool("isMoving", true);

            }else{

                if(PlayerHealthController.instance.gameObject.activeInHierarchy){
                    shootTimeCounter -= Time.deltaTime;

                    if(shootTimeCounter > 0){
                        
                        fireCount -= Time.deltaTime;

                        if(fireCount <= 0){
                            fireCount = fireRate;

                            firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0, 0.0f, 0f));

                            //check angle to the player
                            Vector3 targetDirection = PlayerController.instance.transform.position - transform.position;
                            float angle = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up);

                            if(Mathf.Abs(angle) < 30f){
                                Instantiate(bullet, firePoint.position, firePoint.rotation);
                                anim.SetTrigger("fireShot");
                            }else{
                                shotWaitCounter = waitBetweenShots;
                            }

                            //Instantiate(bullet, firePoint.position, firePoint.rotation);
                        }
                        agent.destination = transform.position;

                    }else{
                        shotWaitCounter = waitBetweenShots;
                    }

                    anim.SetBool("isMoving", false);
                }

                
            }

         

        }

        
    }
}
