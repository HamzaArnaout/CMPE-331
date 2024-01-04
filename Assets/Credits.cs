using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public Animator animator;
    public GameObject mainMenuUI;
    public GameObject creditsUI;
    public GameObject nextButton;

    public void Credit()
    {
        animator.SetTrigger("Credits");
    }

    public void Next()
    {
        animator.SetTrigger("Next");
    }

    public void Back()
    {
        animator.SetTrigger("Back");
    }

    public void ShowMainMenuUI()
    {
        mainMenuUI.SetActive(true);
        creditsUI.SetActive(false);
    }

    public void ShowCreditsUI()
    {
        creditsUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }

    public void DisableNext()
    {
        nextButton.SetActive(false);
    }

    public void EnableNext()
    {
        nextButton.SetActive(true);
    }
}
