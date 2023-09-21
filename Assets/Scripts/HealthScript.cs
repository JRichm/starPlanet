using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    private void Start() {
        currentHealth = maxHealth;
    }

    public void takeDamage(float damage) {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage");
        if (currentHealth < 0) {
            Destroy(gameObject);
        }
    }

    public void addHealth(float health) {
        if (health + currentHealth > maxHealth) {
            currentHealth = maxHealth;
        } else {
            currentHealth += health;
        }
    }

    public void setHealth(float health) {
        if (health > maxHealth) {
            currentHealth = maxHealth;
        } else {
            currentHealth = health;
        }
    }
}
