using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public new Light light;
    void Start()
    {
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        light.enabled = true;
        yield return new WaitForSeconds(1f);
        light.enabled = false;
        yield return new WaitForSeconds(0.1f);
        light.enabled = true;
        yield return new WaitForSeconds(0.1f);
        light.enabled = false;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Flicker());
    }
}
