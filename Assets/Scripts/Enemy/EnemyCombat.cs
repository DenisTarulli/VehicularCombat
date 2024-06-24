using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    public static event Action OnEnemyKill;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;

        if (currentHealth <= 0f)
        {
            OnEnemyKill?.Invoke();
            Destroy(gameObject);
        }
    }
}
