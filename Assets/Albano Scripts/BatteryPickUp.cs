using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickUp : MonoBehaviour, IInteractable
{
    [SerializeField] private FlashlightAdvanced flashlight;
    [SerializeField] private AudioSource pickUpSound;

    public void Interact()
    {
        flashlight.AddBatteries(1);
        //pickUpSound.Play();
        Destroy(gameObject);
    }

    public string SetInteractableText()
    {
        return "Pick up (E) the battery";
    }

}
