using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float explosionEffectDuration;
    [SerializeField] private float destroyDelay;
    [SerializeField] private float radius;
    [SerializeField] private float force;
    [SerializeField] private float damage;
    [SerializeField] private float damageRadius;
    [SerializeField] private string tagToIgnore;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagToIgnore)) return;

        Explode();
    }

    private void Explode()
    {
        GameObject hit = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(hit, explosionEffectDuration);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObjects in colliders)
        {
            if (nearbyObjects.TryGetComponent<Rigidbody>(out var rb) && !nearbyObjects.gameObject.CompareTag("Bullet"))
                rb.AddExplosionForce(force, transform.position, radius);

            if (nearbyObjects.TryGetComponent<PlayerCombat>(out var player))
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance < 1f)
                    distance = 1f;
                if (!(distance > damageRadius))                
                    player.TakeDamage(damage * (1f / distance));
            }

            else if (nearbyObjects.TryGetComponent<EnemyCombat>(out var enemy))
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < 1f)
                    distance = 1f;
                if (!(distance > damageRadius))
                    enemy.TakeDamage(damage * (1f / distance));
            }
        }

        Destroy(gameObject);
    }
}
