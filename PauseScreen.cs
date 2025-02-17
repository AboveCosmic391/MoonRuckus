using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public string mainMenuScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resume()
    {
        GameManager.instance.PauseUnpause();
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting game from Pause Menu");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
