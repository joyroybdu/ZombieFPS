using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // public Camera playerCamera; // Reference to the player's camera
    public bool isShooting, readyToShoot; // Flags to check if the weapon is shooting and ready to shoot
    bool allowReset = true; // Flag to allow resetting the shooting state
    public float shootingDelay = 2f;
    public int bulletperBurst = 3;
    public int currenBurst; // Number of bullets to shoot
    public float spreadIntensity;
    public GameObject bulletPrefab; // Prefab of the bullet to be instantiated
    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletPrefabLifeTime = 3f;

    public enum ShootingMode { Single, Burst, Auto } // Enum to define shooting modes
    public ShootingMode currentShootingMode; // Current shooting mode
      public GameObject muzzleEffect;

    public void Awake()
    {
        readyToShoot = true; // Set the weapon to be ready to shoot at the start
        currenBurst = bulletperBurst;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentShootingMode == ShootingMode.Auto)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0); // Check if the left mouse button is held down for auto shooting
        }
        else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0); // Check if the left mouse button is pressed for single or burst shooting
        }

        if (isShooting && readyToShoot) // If the weapon is shooting and ready to shoot
        {
            currenBurst = bulletperBurst; // Reset the current burst count
            FireWeapon(); // Fire the weapon
        }
    }

    private void FireWeapon()

    {
      muzzleEffect.GetComponent<ParticleSystem>().Play();
      SoundManager.Instance.shootingSound.Play(); 


        readyToShoot = false; // Set the weapon to not ready to shoot

        // Instantiate the bullet at the bullet spawn position and rotation
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        // Calculate shoot direction with spread
        Vector3 shootDirection = CalculateDirectionAndSpread().normalized;

        // Set the bullet's forward direction to the calculated direction
        bullet.transform.forward = shootDirection;

        // Get the Rigidbody component of the bullet and add force
        bullet.GetComponent<Rigidbody>().AddForce(shootDirection * bulletVelocity, ForceMode.Impulse);

        // Destroy the bullet after a certain time to prevent cluttering the scene
        StartCoroutine(DestroyBullet(bullet, bulletPrefabLifeTime));

        if (allowReset) // If resetting is allowed
        {
            Invoke("ResetShooting", shootingDelay);
            allowReset = false; // Set the flag to not allow resetting again
        }

        if (currentShootingMode == ShootingMode.Burst && currenBurst > 1)
        {
            currenBurst--;
            Invoke("FireWeapon", shootingDelay);
        }
    }

    private void ResetShooting()
    {
        readyToShoot = true; // Set the weapon ready again
        allowReset = true;   // Allow resetting again
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        Ray ray =Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit)) // Cast a ray from the camera to check for hits
        {
            targetPoint = hit.point; // Get the point where the ray hit an object
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        // Randomly generate a spread vector
        Vector3 spread = new Vector3(
            Random.Range(-spreadIntensity, spreadIntensity),
            Random.Range(-spreadIntensity, spreadIntensity),
            0
        );

        return direction + new Vector3(spread.x, spread.y, 0);
    }

    private IEnumerator DestroyBullet(GameObject bullet, float delay)
    {
        // Wait for the specified time before destroying the bullet
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
