using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Keypad : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;
    public GameObject keypadOB;
    public GameObject hud;
    


    public GameObject animateOB;
    public Animator ANI;


    public TMP_Text textOB;
    public string answer;

    public AudioSource button;
    public AudioSource correct;
    public AudioSource wrong;

    public bool animate;


    void Start()
    {
        keypadOB.SetActive(false);

    }


    public void Number(int number)
    {
        textOB.text += number.ToString();
        button.Play();
    }

    public void Execute()
    {
        if (textOB.text == answer) //if answer is right play sound effect and display text
        {
            correct.Play();
            textOB.text = "CORRECT!";
            StartCoroutine(Correct());
        }
        else //if answer is wrong play sound effect and display text
        {
            wrong.Play();
            textOB.text = "WRONG";
            StartCoroutine(Wrong());
        }


    }

    public void Clear()
    {
        {
            //remove all text and play sound effect
            textOB.text = "";
            button.Play();
        }
    }

    public void Exit()
    {
        //when we exit make the keypad go away and bring everything back
        keypadOB.SetActive(false);
        hud.SetActive(true);
        player.GetComponent<Interactor>().canInteract = true;
        player.GetComponent<PlayerMovement>().canMove = true;
        camera.GetComponent<CinemachinePOVExtension>().canLook = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        if (textOB.text == "CORRECT!" && animate) //if answer is true, animate
        {
            ANI.SetBool("animate", true);
            Debug.Log("its open");
        }


        if(keypadOB.activeInHierarchy) //if keypad is active set everything else in the screen as false
        {
            hud.SetActive(false);
            player.GetComponent<Interactor>().canInteract = false;
            player.GetComponent<PlayerMovement>().canMove = false;
            camera.GetComponent<CinemachinePOVExtension>().canLook = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }

    IEnumerator Wrong()
    {
        yield return new WaitForSeconds(1f);
        Clear();
    }

    IEnumerator Correct()
    {
        yield return new WaitForSeconds(1f);
        Exit();
    }
}
