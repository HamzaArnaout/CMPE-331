using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

interface IInteractable
{
    public void Interact();
    public string SetInteractableText();
}

public class Interactor : MonoBehaviour
{
    public Transform interactorSource; // Get the transform of the interactor (player most likely)
    public float interactionRange; // Determines distance between player and interactable
    public TMP_Text interactionText;
    public InputManager inputManager;

    void Update()
    {
        Ray r = new Ray(interactorSource.position, interactorSource.forward); // Raycast from player to forward vector

        if (Physics.Raycast(r, out RaycastHit hitInfo, interactionRange)) // Shoot raycast and get info
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                interactionText.text = interactObj.SetInteractableText();
                if (inputManager.PlayerInteractedThisFrame())
                {
                    interactObj.Interact();
                }
            }
            else
            {
                interactionText.text = "";
            }
        }

    }

}
