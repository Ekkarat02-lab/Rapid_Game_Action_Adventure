using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BaseGun : MonoBehaviour
{
    public static BaseGun instance;
    public int bulletsAmount;
    public int maxBullets;
    public int magazineAmount;
    public float reloadTime = 1.5f;
    public float fireRate = 0.5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private float timer;
    public bool isReloading;
    public bool canShoot;
    AudioManager audioManager;
    private void Awake()
    {
        instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        canShoot = false;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse1))
        { 
            canShoot = true;
            if (Input.GetKey(KeyCode.Mouse0) && timer >= fireRate && bulletsAmount > 0 && !isReloading && canShoot)
            {            
                Shoot();
                audioManager.PlaySFX(audioManager.gunfire);
                timer = 0;
            }
            if(Input.GetKeyUp(KeyCode.Mouse1))
            {
                canShoot = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && magazineAmount > 0 && bulletsAmount < maxBullets && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    { 
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletsAmount -= 1;
    }
    IEnumerator Reload()
    {
        isReloading = true;
        canShoot= false;
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
        canShoot = true;
    }
    public void ChangeValue(float value)
    {
        fireRate += value;
         
    }
}
