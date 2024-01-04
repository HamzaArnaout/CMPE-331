using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalBox : MonoBehaviour, IInteractable
{
    private bool canBeInteractedWith = true;
    public Light[] lightsToTurnRed;
    public Light[] lightsToTurnOff;
    public Light doorLight;
    public MeshRenderer doorLightObject;
    public Material doorLightMaterial;

    public Animator animator;

    public AudioSource audioSource;
    public AudioClip powerdownSFX;
    public void Open()
    {
        animator.SetTrigger("Open");
    }

    public void Interact()
    {
        if (canBeInteractedWith) {
            foreach(Light light in lightsToTurnRed)
            {
                light.enabled = true;
                light.color = Color.red;
            }

            foreach (Light light in lightsToTurnOff)
            {
                light.enabled = false;
            }

            doorLight.color = Color.green;
            doorLightObject.material = doorLightMaterial;
            Open();

            audioSource.PlayOneShot(powerdownSFX);

            canBeInteractedWith = false;
        }    
    }

    public string SetInteractableText()
    {
        if (canBeInteractedWith)
            return "Turn (E) Power Off";
        else
            return "";
    }
}
