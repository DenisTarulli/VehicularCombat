using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject damagedEffect;
    [SerializeField] private GameObject highDamagedEffect;
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private float destroyEffectLifetime;
    private float currentHealth;

    public static event Action OnEnemyKill;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;
        DamagedVisualEffect();

        if (currentHealth <= 0f)
        {
            OnEnemyKill?.Invoke();
            GameObject deathExplosion = Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(deathExplosion, destroyEffectLifetime);
            Destroy(gameObject);
        }
    }

    private void DamagedVisualEffect()
    {
        if (currentHealth <= (maxHealth / 3f))
            highDamagedEffect.SetActive(true);
        else if (currentHealth <= (maxHealth * 2f / 3f))
            damagedEffect.SetActive(true);
    }
}
