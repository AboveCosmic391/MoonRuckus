using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroExit : MonoBehaviour
{
     public string sceneToLoad = "MainMenu";
    public Animator animator;
    private bool animationComplete = false;

    void Start()
    {
        // Get the Animator component attached to the GameObject
        animator = GetComponent<Animator>();

        // Start the animation
        animator.Play("TextMove");
    }

    void Update()
    {
        // Check if the animation has completed
        if (!animationComplete && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            // Set the flag to indicate that the animation has completed
            animationComplete = true;

            // Load the new scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
