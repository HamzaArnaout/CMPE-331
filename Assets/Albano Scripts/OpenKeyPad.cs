using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenKeyPad : MonoBehaviour, IInteractable
{
    public GameObject keypadOB;
    public InputManager inputManager;

    public void Interact()
    {
        keypadOB.SetActive(true);
    }

    public string SetInteractableText()
    {
        return "Use (E) KeyPad";
    }
}
