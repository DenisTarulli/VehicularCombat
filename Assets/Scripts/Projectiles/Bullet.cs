using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float explosionEffectDuration;
    [SerializeField] private float destroyDelay;
    [SerializeField] private float radius;
    [SerializeField] private float force;
    [SerializeField] private float damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;

        Explode();
    }

    private void Explode()
    {
        GameObject hit = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(hit, explosionEffectDuration);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObjects in colliders)
        {
            if (nearbyObjects.TryGetComponent<Rigidbody>(out var rb))
                rb.AddExplosionForce(force, transform.position, radius);
        }

        Destroy(gameObject);
    }
}
