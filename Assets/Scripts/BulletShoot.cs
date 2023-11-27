using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 1000f; // Mermi hızı, bu değeri ihtiyacınıza göre ayarlayabilirsiniz
    public InputManager inputManager;

    void Update()
    {
        if (inputManager.PlayerShotThisFrame())
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * bulletSpeed);
        }

        StartCoroutine(DeleteBullet(bullet));
    }

    IEnumerator DeleteBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(2f);
        Destroy(bullet);
    }
}
