using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 10f;
    public float impactForce = 100f;
    public int maxAmmo = 10;
    public float reloadTime = 3f;
    public float recoilRate = 3f;

    public PlayerControl playerController;
    public Animator playerAnimator;
    public MouseLook mouseLook;
    public Camera playerCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Animator animator;
    public PlayerUI playerHud;

    public SpawnTarget targetSpawner; // Temporary for testing

    private int currentAmmo;
    private float nextTimeToFire = 0f;
    private bool isReloading = false;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        playerHud.ammoHud.UpdateAmmoUI(currentAmmo, playerController.ammoCount);
    }

    // OnEnable is called when re-enabling the object
    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
        playerHud.ammoHud.UpdateCurrentAmmo(currentAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading == true)
        {
            return;
        }

        if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            if (playerController.ammoCount > 0 && currentAmmo != maxAmmo)
            {
                StartCoroutine(Reload());
            }
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && !playerAnimator.GetCurrentAnimatorStateInfo(1).IsName("Weapon_Sprinting"))
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            mouseLook.Recoil(recoilRate);
        }
        
        // Temporary for testing
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
            {
                targetSpawner.SpawnWoodenCrate(hit.point, 100);
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        
        Debug.Log("Reloading");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime);

        currentAmmo += playerController.SpendAmmo(maxAmmo - currentAmmo, false);
        playerHud.ammoHud.UpdateCurrentAmmo(currentAmmo);

        animator.SetBool("Reloading", false);

        Debug.Log("Reloaded");

        isReloading = false;
    }

    void Shoot()
    {
        muzzleFlash.Play();

        currentAmmo--;
        playerHud.ammoHud.UpdateCurrentAmmo(currentAmmo);

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            //Hit something with ray
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.ReceiveDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            
            GameObject impactTemp = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactTemp, 1f);
        }
    }
}
