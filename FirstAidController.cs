using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAidController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            Debug.Log("picked up a first aid pack");
            PlayerHealthController.instance.IncreasePlayerHealth(1);
            Destroy(gameObject);
        }
        
    }
}
