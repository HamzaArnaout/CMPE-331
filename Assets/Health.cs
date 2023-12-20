using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

interface IDamageable
{
    public void TakeDamage(float Amount);
    public void RegenHealth();
}
public class Health : MonoBehaviour, IDamageable
{
    public float health = 20f;
    public float maxHealth = 20f;
    //public GameObject bloodOverlay;

    public float cooldown = 5f;
    public float maxCooldown = 5f;

    public Animator animator;
    public GameObject bloodOverlay;

    public void Update()
    {
        if (health <= 0f)
        {
            Die();
        }

        if (health < maxHealth)
        {
            if(bloodOverlay != null) {
                bloodOverlay.SetActive(true);
            }
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                RegenHealth();
                cooldown = maxCooldown;
            }
        }

        else if (health >= maxHealth && bloodOverlay != null)
        {
            bloodOverlay.SetActive(false);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    public void RegenHealth()
    {
        if (health <= maxHealth)
        {
            health += maxHealth / 2;
        }
    }

    public void Die()
    {
        if(animator != null)
        {
            animator.SetTrigger("Die"); // trigger the death animation if theres an animator on the gameobject
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
