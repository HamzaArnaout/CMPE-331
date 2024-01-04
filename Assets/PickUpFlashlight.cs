using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFlashlight : MonoBehaviour, IInteractable
{
    public Inventory inventory;
    public GameObject flashlightUI;

    public void Interact()
    {
        inventory.canEquipFlashlight = true;
        inventory.currentEquipped = 1;
        flashlightUI.SetActive(true);
        Destroy(gameObject);
    }

    public string SetInteractableText()
    {
        return "Pick Up (E) The Flashlight";
    }
}
