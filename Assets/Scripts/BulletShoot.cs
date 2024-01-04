using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.HighDefinition;

public class BulletShoot : MonoBehaviour
{
    public Transform firePoint;
    private float bulletRange = 1000f;
    private bool canShoot = true;

    // Text element to display the bullet count
    public TextMeshProUGUI bulletText;
    // Number of bullets currently ready to fire
    [SerializeField] private int bulletCount = 12;
    // Total number of bullets available in the inventory
    [SerializeField] private int bulletInventoryCount = 48;
    private bool reloading = false;

    [SerializeField] InputManager inputManager;
    [SerializeField] ParticleSystem muzzleFlashParticle;
    [SerializeField] Light muzzleFlashLight;

    public Interactor interactor;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip gunShotSFX;
    [SerializeField] AudioClip gunReloadSFX;

    Ray r;

    public Animator animator;
    public GameObject gun;
    public GameObject gunUI;

    void Update()
    {
        if (gun.activeInHierarchy)
        {
            gunUI.SetActive(true);

            r = new Ray(firePoint.position, firePoint.forward); // Raycast from player to forward vector

            if (interactor.canInteract && !reloading)
            {
                // If the player shoots, and has bullets in the magazine,
                // we shoot, reduce 1 bullet, and update the text
                if (inputManager.PlayerShotThisFrame() && bulletCount > 0 && canShoot)
                {
                    StartCoroutine(FireBullet());
                    bulletCount--;
                    UpdateBulletText();
                }

                // If the player reloads and has bullets in the inventory
                // We do the math to check the bullets to remove from the inventory
                // and the bullets to add to the magazine
                if (inputManager.PlayerReloadedThisFrame() && bulletInventoryCount > 0 && bulletCount < 12)
                {
                    // Calculate how many bullets are needed to refill the magazine to full
                    StartCoroutine(Reloading());
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
        else
        {
            gunUI.SetActive(false);
        }
    }

    IEnumerator Reloading()
    {
        reloading = true;
        audioSource.PlayOneShot(gunReloadSFX);
        yield return new WaitForSeconds(2f);
        reloading = false;
    }

    // Updates the text display showing the number of bullets
    void UpdateBulletText()
    {
        // Show the inventory count and the ready bullet count
        bulletText.text = bulletCount.ToString() + "/" +bulletInventoryCount;
    }

    // Function to handle the firing of a bullet
    IEnumerator FireBullet()
    {

        if (Physics.Raycast(r, out RaycastHit hitInfo, bulletRange)) // Shoot raycast and get info
        {
            Debug.Log(hitInfo.collider.name);
            if (hitInfo.collider.tag == "Zombie")
            {
                Debug.Log(hitInfo.collider.gameObject.GetComponentInParent<Health>().health);
                hitInfo.collider.gameObject.GetComponentInParent<Health>().TakeDamage(10f);
                Debug.Log("Shot");
            }
            else if (hitInfo.collider.tag == "ZombieHead")
            {
                hitInfo.collider.gameObject.GetComponentInParent<Health>().TakeDamage(100f);
                Debug.Log("HEADSHOT!");
            }
        }

        StartCoroutine(MuzzleFlashLight());
        muzzleFlashParticle.Play();

        animator.SetTrigger("Recoil");

        audioSource.PlayOneShot(gunShotSFX);
        canShoot = false;
        yield return new WaitForSeconds(.25f);
        canShoot = true;
    }

    IEnumerator MuzzleFlashLight()
    {
        muzzleFlashLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        muzzleFlashLight.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = firePoint.TransformDirection(Vector3.forward) * 1000;
        Gizmos.DrawRay(firePoint.position, direction);
    }
}
