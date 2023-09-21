using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider enemyCollider;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        enemyCollider = GetComponent<BoxCollider>();
    }

    private void OnColliderEnter(Collider other) {
        if (other != null) {
            Debug.Log("on collider enter");
            Debug.Log(other);
        }
    }
}
