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
    private int bulletCount = 30;
    // Total number of bullets available in the inventory
    private int bulletInventoryCount = 120;

    void Update()
    {
        // Check if the left mouse button is clicked and there are bullets to fire
        if (Input.GetMouseButtonDown(0) && bulletCount > 0)
        {
            // Fire a bullet
            FireBullet();
            // Decrease the count of ready bullets
            bulletCount--;
            // Update the bullet count display
            UpdateBulletText();
        }

        // Check if the 'R' key is pressed and there are bullets in the inventory
        if (Input.GetKeyDown(KeyCode.R) && bulletInventoryCount > 0)
        {
            // Calculate how many bullets are needed to refill the magazine to full
            int neededBullets = 30 - bulletCount;
            // Determine how many bullets can be reloaded from the inventory
            int bulletsToReload = Mathf.Min(neededBullets, bulletInventoryCount);
            // Decrease the inventory count by the number of bullets reloaded
            bulletInventoryCount -= bulletsToReload;
            // Increase the ready bullet count by the number of bullets reloaded
            bulletCount += bulletsToReload;
            // Update the bullet count display
            UpdateBulletText();
        }
    }

    // Updates the text display showing the number of bullets
    void UpdateBulletText()
    {
        // Show the inventory count and the ready bullet count
        bulletText.text = bulletInventoryCount + "/" + bulletCount.ToString();
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
        // Destroy the bullet after 5 seconds
        Destroy(bullet, 5.0f);
    }
}
