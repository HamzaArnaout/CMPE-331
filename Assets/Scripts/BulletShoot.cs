using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletShoot : MonoBehaviour
{
    // Define the bullet prefab to be instantiated
    public GameObject bulletPrefab;
    // The transform from which bullets will be fired
    public Transform firePoint;
    // Speed of the bullet
    public float bulletSpeed = 1000f;

    // Text element to display bullet count
    public TextMeshProUGUI bulletText;
    // Initial bullet count
    private int bulletCount = 30;

    void Update()
    {
        // Check if left mouse button is clicked and if there are bullets left
        if (Input.GetMouseButtonDown(0) && bulletCount > 0)
        {
            // Fire a bullet
            FireBullet();
            // Decrement the bullet count
            bulletCount--;
            // Update the bullet count display
            UpdateBulletText();
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletCount == 0)
        {
            // Reset the bullet count to 30
            bulletCount = 30;
            // Update the bullet count display
            UpdateBulletText();
        }
    }

    // Update the bullet count text on the UI
    void UpdateBulletText()
    {
        bulletText.text = bulletCount.ToString();
    }

    // Method to fire a bullet
    void FireBullet()
    {
        // Create a bullet instance at the fire point's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Get the Rigidbody component of the bullet for physics
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Apply force to the bullet to propel it forward
            rb.AddForce(firePoint.forward * bulletSpeed);
        }
    }
}
