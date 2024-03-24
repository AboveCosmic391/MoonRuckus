using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthController : MonoBehaviour
{

    public static UIHealthController instance;

    public Slider healthSlider;
    public Text healthText;
    public GameObject pauseScreen;
    public Text shotsFired;
    public Text hitsAchieved;
    public Text headshotsAchieved;
    public Text hitsTaken;

    public Text accuracy;
    public Text headshotAccuracy;


    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
