using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipWeapon : MonoBehaviour
{
    [SerializeField] float fireRate = 0.002f;
    [SerializeField] float spreadAmount = 0.025f;
    [SerializeField] int maxAmmo = 30;
    [SerializeField] int currentAmmo;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] TextMeshProUGUI ammoDisplay;

    private float nextFireTime;

    private void Start() {
        shootPoint = GetComponentInParent<HeloScript>().shipFirePosition;
        currentAmmo = maxAmmo;
    }

    public void Shoot() {

        if (currentAmmo > 0 && Time.time > nextFireTime) {

            Quaternion randomRotation = Quaternion.Euler(
                Random.Range(-spreadAmount, spreadAmount),
                Random.Range(-spreadAmount, spreadAmount),
                0f
            );

            Quaternion finalRotation = shootPoint.rotation * randomRotation;

            // instantiate and shoot a bullet
            Instantiate(bulletPrefab, shootPoint.position, finalRotation);

            // update the next fire time based on the fire rate
            nextFireTime = Time.time + fireRate;

            // reduce ammo count
            currentAmmo--;
        }

        ammoDisplay.text = currentAmmo.ToString() + " / " + maxAmmo.ToString();
    }

    public void CoolWeapon() {
        if (currentAmmo < maxAmmo && Time.time > nextFireTime) {
            nextFireTime = Time.time + fireRate;

            currentAmmo++;
        }

        ammoDisplay.text = currentAmmo.ToString() + " / " + maxAmmo.ToString();
    }
}
