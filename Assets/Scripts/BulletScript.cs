using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 100f;
    [SerializeField] ParticleSystem explosionEffect;

    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other) {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
