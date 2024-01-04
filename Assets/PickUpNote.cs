using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpNote : MonoBehaviour, IInteractable
{
    public Inventory inventory;
    public GameObject noteUI;

    public void Interact()
    {
        inventory.canEquipNote = true;
        inventory.currentEquipped = 3;
        noteUI.SetActive(true);
        Destroy(gameObject);
    }

    public string SetInteractableText()
    {
        return "Pick Up (E) The Note";
    }
}
