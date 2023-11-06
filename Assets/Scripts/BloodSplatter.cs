using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BloodSplatter : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("There's blood on the wall");
    }

    public string SetInteractableText()
    {
        string text = "Inspect (E) the blood";
        return text;
    }
}
