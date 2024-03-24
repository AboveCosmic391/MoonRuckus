using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;

    public int maxHealth, currentHealth;

    public float invincibleLength = 1f;
    private float invincibleCounter;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UIHealthController.instance.healthSlider.maxValue = maxHealth;
        UIHealthController.instance.healthSlider.value = currentHealth;
        UIHealthController.instance.healthText.text = "HEALTH: " + currentHealth + "/" + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleCounter > 0){
            invincibleCounter -= Time.deltaTime;
        }
    }

    public void DamagePlayer(int amount)
    {
        if(invincibleCounter <= 0){
            currentHealth -= amount;
            Debug.Log(currentHealth);
            if(currentHealth <= 0)
            {
                gameObject.SetActive(false);
                currentHealth = 0;

                GameManager.instance.PlayerDies();
            }

            invincibleCounter = invincibleLength;

            UIHealthController.instance.healthSlider.value = currentHealth;
            UIHealthController.instance.healthText.text = "HEALTH: " + currentHealth + "/" + maxHealth;
        }
        
    }

    public void IncreasePlayerHealth(int amount){
        if(currentHealth < maxHealth){
            currentHealth += amount;
            UIHealthController.instance.healthSlider.value = currentHealth;
            UIHealthController.instance.healthText.text = "HEALTH: " + currentHealth + "/" + maxHealth;
        }
    }
}
