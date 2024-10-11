using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BaseGun : MonoBehaviour
{
    public int bulletsAmount;
    public int maxBullets;
    public int magazineAmount;
    public float reloadTime = 1.5f;
    public float fireRate = 0.5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private float timer;
    private bool isReloading;
    // Update is called once per frame
    void Update()
    {
        RotateGunTowardsCursor();
        timer += Time.deltaTime;
        if(Input.GetKey(KeyCode.Mouse0) && timer >= fireRate && bulletsAmount > 0 && !isReloading)
        {
            Shoot();
            timer = 0;
        }
        if (Input.GetKeyDown(KeyCode.R) && magazineAmount > 0 && bulletsAmount < maxBullets && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }
    void RotateGunTowardsCursor()
    {
        // Get the mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get the direction from the gun to the mouse
        Vector3 direction = mousePos - transform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the gun
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    void Shoot()
    { 
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletsAmount -= 1;
    }
    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        int bulletToReload = maxBullets - bulletsAmount;

        if(magazineAmount > 0)
        {
            if(magazineAmount > 1)
            {
                bulletsAmount = maxBullets;
                magazineAmount -= 1;
            }
        }
        
        isReloading = false;
    }
}
