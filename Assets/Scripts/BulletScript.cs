using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 100f;
    [SerializeField] ParticleSystem explosionEffect;
    [SerializeField] int bulletDamage = 10;

    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
    }

    private void OnCollisionEnter(Collision collision) {
        ContactPoint contact = collision.contacts[0]; // Get the first contact point.
        HealthScript otherHealthScript = collision.gameObject.GetComponent<HealthScript>();
        if (otherHealthScript != null) {
            otherHealthScript.takeDamage((float)bulletDamage);
        }

        // Calculate a position slightly above the collision point.
        Vector3 explosionPosition = contact.point + contact.normal * 0.1f; // You can adjust the offset as needed.

        // Instantiate the explosion effect at the calculated position.
        Instantiate(explosionEffect, explosionPosition, Quaternion.identity);

        // Destroy the bullet.
        Destroy(gameObject);
    }
}
