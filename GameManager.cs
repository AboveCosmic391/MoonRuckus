using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public float waitAfterDying = 2f;




    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            PauseUnpause();
        }
    }

    public void PlayerDies(){
        Debug.Log("Player lost too much health");
        
        StartCoroutine(playerDiedRespawn());
    }

    public IEnumerator playerDiedRespawn(){
        yield return new WaitForSeconds(waitAfterDying);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseUnpause()
    {
        // game is resumed
        if(UIHealthController.instance.pauseScreen.activeInHierarchy)
        {
            UIHealthController.instance.pauseScreen.SetActive(false);
            // lock the cursor
            Cursor.lockState = CursorLockMode.Locked;
            // resume all time progress in game 
            Time.timeScale = 1f;
        }
        // game is paused
        else
        {
            UIHealthController.instance.pauseScreen.SetActive(true);
            // unlock the cursor
            Cursor.lockState = CursorLockMode.None;

            // freeze all time in the game
            Time.timeScale = 0f;

            int hitsAchieved = PlayerPrefs.GetInt("hitsAchieved");
            int shotsFired = PlayerPrefs.GetInt("shotsFired");
            int headshotsAchieved = PlayerPrefs.GetInt("headshotAchieved");
            float headshotAccuracy = headshotsAchieved / hitsAchieved * 100;
            float accuracy = hitsAchieved / shotsFired * 100;

            // UIHealthController.instance.shotsFired.text = PlayerPrefs.GetInt("shotsFired").ToString();
            UIHealthController.instance.shotsFired.text = shotsFired.ToString();
            UIHealthController.instance.hitsAchieved.text = PlayerPrefs.GetInt("hitsAchieved").ToString();
            UIHealthController.instance.headshotsAchieved.text = PlayerPrefs.GetInt("headshotAchieved").ToString();
            UIHealthController.instance.hitsTaken.text = PlayerPrefs.GetInt("timesHit").ToString();
            UIHealthController.instance.accuracy.text = accuracy.ToString() + "%";
            UIHealthController.instance.headshotAccuracy.text = headshotAccuracy.ToString() + "%";
            Debug.Log("Shots fired: " + PlayerPrefs.GetInt("shotsFired"));
            Debug.Log("Hits achieved: " + PlayerPrefs.GetInt("hitsAchieved"));
            Debug.Log("Headshots achieved: " + PlayerPrefs.GetInt("headshotAchieved"));
            Debug.Log("Hits taken: " + PlayerPrefs.GetInt("timesHit"));
            Debug.Log("Accuracy: " + accuracy);
            Debug.Log("Headshot Accuracy: " + headshotAccuracy);
        }
        
    }
}
