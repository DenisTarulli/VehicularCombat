using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    private bool canTakeDamage;

    public static event Action<float> OnHurt;
    public float CurrentHealth { get => currentHealth; }
    public float MaxHealth { get => maxHealth; }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        canTakeDamage = true;
    }

    public void TakeDamage(float damageToTake)
    {
        if (!canTakeDamage) return;

        currentHealth -= damageToTake;
        OnHurt?.Invoke(currentHealth);

        if (currentHealth <= 0f)
        {
            // a
        }
    }
}
