using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletShoot : MonoBehaviour
{
    // References to the bullet prefab and the firing point
    public GameObject bulletPrefab;
    public Transform firePoint;
    // Bullet speed
    public float bulletSpeed = 1000f;

    // Text element to display the bullet count
    public TextMeshProUGUI bulletText;
    // Number of bullets currently ready to fire
    [SerializeField] private int bulletCount = 12;
    // Total number of bullets available in the inventory
    [SerializeField] private int bulletInventoryCount = 48;

    [SerializeField] InputManager inputManager;
    [SerializeField] ParticleSystem muzzleFlashParticle;
    [SerializeField] Light muzzleFlashLight;

    public Interactor interactor;

    void Update()
    {
        if (interactor.canInteract)
        {
            // If the player shoots, and has bullets in the magazine,
            // we shoot, reduce 1 bullet, and update the text
            if (inputManager.PlayerShotThisFrame() && bulletCount > 0)
            {
                FireBullet();
                bulletCount--;
                UpdateBulletText();
            }

            // If the player reloads and has bullets in the inventory
            // We do the math to check the bullets to remove from the inventory
            // and the bullets to add to the magazine
            if (inputManager.PlayerReloadedThisFrame() && bulletInventoryCount > 0)
            {
                // Calculate how many bullets are needed to refill the magazine to full
                int neededBullets = 12 - bulletCount;
                // Determine how many bullets can be reloaded from the inventory
                int bulletsToReload = Mathf.Min(neededBullets, bulletInventoryCount);
                // Decrease the inventory count by the number of bullets reloaded
                bulletInventoryCount -= bulletsToReload;
                // Increase the ready bullet count by the number of bullets reloaded
                bulletCount += bulletsToReload;
                UpdateBulletText();
            }
        }
    }

    // Updates the text display showing the number of bullets
    void UpdateBulletText()
    {
        // Show the inventory count and the ready bullet count
        bulletText.text = bulletCount.ToString() + "/" +bulletInventoryCount;
    }

    // Function to handle the firing of a bullet
    void FireBullet()
    {
        // Create a bullet instance at the fire point's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Get the Rigidbody component for applying physics
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Apply a force to the bullet to propel it forward
            rb.AddForce(firePoint.forward * bulletSpeed);
        }
        StartCoroutine(MuzzleFlashLight());
        muzzleFlashParticle.Play();
        // Destroy the bullet after 5 seconds
        Destroy(bullet, 5.0f);
    }

    IEnumerator MuzzleFlashLight()
    {
        muzzleFlashLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        muzzleFlashLight.enabled = false;
    }
}
