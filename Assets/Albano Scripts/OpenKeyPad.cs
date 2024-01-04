using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenKeyPad : MonoBehaviour, IInteractable
{
    public GameObject keypadUI;
    public InputManager inputManager;
    public Keypad keypad;

    public void Interact()
    {
        if(keypad.canBeInteractedWith)
            keypadUI.SetActive(true);
    }

    public string SetInteractableText()
    {
        if (keypad.canBeInteractedWith)
            return "Use (E) KeyPad";
        else
            return "";
    }
}
