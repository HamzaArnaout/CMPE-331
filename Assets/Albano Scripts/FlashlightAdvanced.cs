using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlashlightAdvanced : MonoBehaviour
{
    private new Light light;

    [Header("Text elements")]
    public TMP_Text lifetimeText;
    [SerializeField] private TMP_Text batteriesAmountText;

    [Header("Properties")]
    [SerializeField] private float lifetime;
    [SerializeField] private float batteriesAmount = 0;
    [SerializeField] private bool isFlashlightOn;

    [Header("Sound")]
    [SerializeField] private AudioSource flashON;
    [SerializeField] private AudioSource flashOFF;

    [Header("Controls")]
    public InputManager inputManager;


    void Start()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {
        // Update the text for lifetime and battery amount respectively
        lifetimeText.text = lifetime.ToString("0") + "%";
        batteriesAmountText.text = batteriesAmount.ToString();


        // Turning on the flashlight
        if(inputManager.PlayerActivatedFlashlightThisFrame() && !isFlashlightOn)
        {
            //flashON.Play();
            light.enabled = true;
            isFlashlightOn = !isFlashlightOn;
        }

        // Turning off the flashlight
        else if (inputManager.PlayerActivatedFlashlightThisFrame() && isFlashlightOn)
        {
            //flashOFF.Play();
            light.enabled = false;
            isFlashlightOn = !isFlashlightOn;
        }

        // As long as the flashlight is turned on
        // lifetime reduces by 1 each second
        if (isFlashlightOn && lifetime > 0)
        {
            lifetime -= 1 * Time.deltaTime;
        }

        // Cap lifetime from ever going less than 0
        // and shutting off the flashlight if the
        // lifetime is equal to 0
        if(lifetime <= 0)
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
        if (inputManager.PlayerReloadedThisFrame() && batteriesAmount >= 1)
        {
            batteriesAmount -= 1;
            lifetime += 50;
        }
    }

    public void AddBatteries(int amountToAdd)
    {
        batteriesAmount += amountToAdd;
    }
}
