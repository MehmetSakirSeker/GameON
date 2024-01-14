using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WeaponMethods weaponMethods;

    [SerializeField] private Transform muzzle;
    private float timeSinceLastShot;
    private UIManager uiManager;

    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
        uiManager = GameObject.Find("AmmoCanvas").GetComponent<UIManager>();
    }

    public void StartReload()
    {
        if (this != null)
        {
            if (!weaponMethods.reloading)
            {
                StartCoroutine(Reload());
            }
        }
    }

    private IEnumerator Reload()
    {
        if (this != null) 
        {
            weaponMethods.reloading = true;
            yield return new WaitForSeconds(weaponMethods.reloadTime);
            weaponMethods.currentAmmo = weaponMethods.magSize;
            uiManager.UpdateAmmo(weaponMethods.currentAmmo);
            weaponMethods.reloading = false;
        }
    }

    private bool CanShoot() => !weaponMethods.reloading && timeSinceLastShot > 1f / (weaponMethods.fireRate / 60f);

    private void Shoot()
    {
        if (this != null && weaponMethods != null && muzzle != null)
        {
            if (weaponMethods.currentAmmo > 0)
            {
                if (CanShoot())
                {
                    if (Physics.Raycast(muzzle.position, transform.forward, out RaycastHit hitInfo, weaponMethods.range))
                    {
                        EnemyTakeDamage giveDamage = hitInfo.transform.GetComponent<EnemyTakeDamage>();
                        giveDamage?.TakeDamage(weaponMethods.damage);
                    }
                    Debug.Log("weapon");
                    uiManager.UpdateAmmo(weaponMethods.currentAmmo);
                    weaponMethods.currentAmmo--;
                    timeSinceLastShot = 0;
                    OnWeaponShot();
                }
            }
        }
    }

    private void Update()
    {
        if (this != null)
        {
            timeSinceLastShot += Time.deltaTime;
        }
    }

    private void OnWeaponShot()
    {

    }
}
