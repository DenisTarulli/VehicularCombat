using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageToTake)
    {
        Debug.Log($"Daño a enemigo: {damageToTake}");
        currentHealth -= damageToTake;

        if (currentHealth <= 0f)
        {
            // a
        }
    }
}
