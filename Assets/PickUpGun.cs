using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGun : MonoBehaviour, IInteractable
{
    public Inventory inventory;
    public GameObject gunUI;

    public void Interact()
    {
        inventory.canEquipGun = true;
        inventory.currentEquipped = 2;
        gunUI.SetActive(true);
        Destroy(gameObject);
    }

    public string SetInteractableText()
    {
        return "Pick Up (E) The Gun";
    }
}
