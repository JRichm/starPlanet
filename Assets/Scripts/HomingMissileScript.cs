using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HomingMissileScript : MonoBehaviour
{
    [SerializeField] float missleThrust;
    [SerializeField] float targetLockSensitivity;

    private Rigidbody rb;
    private CapsuleCollider col;    

    private void Start() {
        rb = GetComponent<Rigidbody>();
        col = GetComponentInChildren<CapsuleCollider>();
    }

    private void Update() {
        rb.AddForceAtPosition(-transform.up * missleThrust, Vector3.zero, ForceMode.Force); ;
    }
}
