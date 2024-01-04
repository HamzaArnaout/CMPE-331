using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlashlightAdvanced : MonoBehaviour
{
    public new Light light;

    [Header("UI elements")]
    public TMP_Text lifetimeText;
    [SerializeField] private TMP_Text batteriesAmountText;
    public Image lifetimeImage;

    [Header("Properties")]
    [SerializeField] private float lifetime;
    [SerializeField] private float batteriesAmount = 0;
    [SerializeField] private bool isFlashlightOn;

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip flashON;
    [SerializeField] private AudioClip flashOFF;

    [Header("Controls")]
    public InputManager inputManager;
    public Interactor interactor;

    [Header("Other")]
    public GameObject flashlight;
    public GameObject flashlightUI;

    void Update()
    {
        if (flashlight.activeInHierarchy)
        {
            flashlightUI.SetActive(true);

            // Update the text for lifetime and battery amount respectively
            lifetimeText.text = lifetime.ToString("0") + "%";
            batteriesAmountText.text = batteriesAmount.ToString();
            lifetimeImageHandling();


            // Turning on the flashlight
            if (inputManager.PlayerActivatedFlashlightThisFrame() && !isFlashlightOn && interactor.canInteract)
            {
                audioSource.PlayOneShot(flashON);
                light.enabled = true;
                isFlashlightOn = !isFlashlightOn;
            }

            // Turning off the flashlight
            else if (inputManager.PlayerActivatedFlashlightThisFrame() && isFlashlightOn && interactor.canInteract)
            {
                audioSource.PlayOneShot(flashOFF);
                light.enabled = false;
                isFlashlightOn = !isFlashlightOn;
            }

            // As long as the flashlight is turned on
            // lifetime reduces by 2 each second
            if (isFlashlightOn && lifetime > 0)
            {
                lifetime -= 2 * Time.deltaTime;
            }

            // Cap lifetime from ever going less than 0
            // and shutting off the flashlight if the
            // lifetime is equal to less than 0.5
            if (lifetime < 0.5)
            {
                light.enabled = false;
                isFlashlightOn = false;
                lifetime = 0;
            }

            // Cap lifetime from ever going over 100
            if (lifetime >= 100)
            {
                lifetime = 100;
            }

            // Reloading batteries makes adds 50 to the lifetime
            // and it deducts 1 battery from the inventory
            if (inputManager.PlayerReloadedThisFrame() && batteriesAmount >= 1 && interactor.canInteract)
            {
                batteriesAmount -= 1;
                lifetime += 100;
            }
        }
        else
        {
            flashlightUI.SetActive(false);
        }
    }

    public void AddBatteries(int amountToAdd)
    {
        batteriesAmount += amountToAdd;
    }

    public void lifetimeImageHandling()
    {
        if (lifetime == 0) lifetimeImage.fillAmount = 0 / 100f;
        else if (lifetime <= 25 && lifetime >= 0.5) lifetimeImage.fillAmount = 25 / 100f;
        else if (lifetime <= 50 && lifetime >= 26) lifetimeImage.fillAmount = 50 / 100f;
        else if (lifetime <= 75 && lifetime >= 51) lifetimeImage.fillAmount = 75 / 100f;
        else if (lifetime >= 100) lifetimeImage.fillAmount = 100 / 100f;
    }
}
