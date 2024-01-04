using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;


public class Keypad : MonoBehaviour
{
    public GameObject player;
    public new GameObject camera;
    public GameObject hud;
    public GameObject keypadHUD;
    public KeypadDoor door;

    public TMP_Text textOB;
    public string answer;
    public bool canBeInteractedWith = true;

    public AudioSource audioSource;
    public AudioClip button;
    public AudioClip correct;
    public AudioClip wrong;

    public void Number(int number)
    {
        textOB.text += number.ToString();
        audioSource.PlayOneShot(button);
    }

    public void Execute()
    {
        if (textOB.text == answer) //if answer is right play sound effect and display text
        {
            audioSource.PlayOneShot(correct);
            textOB.text = "CORRECT!";
            StartCoroutine(Correct());
        }
        else //if answer is wrong play sound effect and display text
        {
            audioSource.PlayOneShot(wrong);
            textOB.text = "WRONG";
            StartCoroutine(Wrong());
        }
    }

    public void Clear()
    {
        //remove all text and play sound effect
        textOB.text = "";
        audioSource.PlayOneShot(button);
    }

    public void Exit()
    {
        //when we exit make the keypad go away and bring everything back
        keypadHUD.SetActive(false);
        hud.SetActive(true);
        player.GetComponent<Interactor>().canInteract = true;
        player.GetComponent<PlayerMovement>().canMove = true;
        camera.GetComponent<CinemachinePOVExtension>().canLook = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        if(keypadHUD.activeInHierarchy) //if keypad hud is active set everything else in the screen as false
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
        canBeInteractedWith = false;
        Exit();
        door.Open();
    }
}
