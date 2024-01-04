using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject endUI;
    public GameObject hudUI;
    public CinemachinePOVExtension cam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hudUI.SetActive(false);
            endUI.SetActive(true);
            cam.canLook = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            other.gameObject.SetActive(false);
        }
    }
}
