using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool canEquipFlashlight;
    public bool canEquipGun;
    public bool canEquipNote;

    public GameObject[] Equipables;

    public int currentEquipped = 0;

    public InputManager inputManager;

    public void Update()
    {
        Vector2 scroll = inputManager.PlayerScrolledThisFrame();

        if (scroll.y > 0)
        {
            if(currentEquipped >= 3)
            {
                currentEquipped = 0;
            } 
            else
                currentEquipped++;
        }

        if (scroll.y < 0)
        {
            if (currentEquipped <= 0)
            {
                currentEquipped = 3;
            }
            else
                currentEquipped--;
        }

        if(Input.GetKey(KeyCode.Alpha1)) {
            currentEquipped = 0;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            currentEquipped = 1;
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            currentEquipped = 2;
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            currentEquipped = 3;
        }

        if (currentEquipped == 0)
        {
            Equipables[0].gameObject.SetActive(true);
            Equipables[1].gameObject.SetActive(false);
            Equipables[2].gameObject.SetActive(false);
            Equipables[3].gameObject.SetActive(false);
        }
        else if (currentEquipped == 1 && canEquipFlashlight)
        {
            Equipables[0].gameObject.SetActive(false);
            Equipables[1].gameObject.SetActive(true);
            Equipables[2].gameObject.SetActive(false);
            Equipables[3].gameObject.SetActive(false);
        }
        else if (currentEquipped == 2 && canEquipGun)
        {
            Equipables[0].gameObject.SetActive(false);
            Equipables[1].gameObject.SetActive(false);
            Equipables[2].gameObject.SetActive(true);
            Equipables[3].gameObject.SetActive(false);
        }
        else if (currentEquipped == 3 && canEquipNote)
        {
            Equipables[0].gameObject.SetActive(false);
            Equipables[1].gameObject.SetActive(false);
            Equipables[2].gameObject.SetActive(false);
            Equipables[3].gameObject.SetActive(true);
        }
    }
}
