using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float bulletForce;
    [SerializeField] private float fireRate;
    [SerializeField] private float bulletLifeTime;
    private float nextTimeToShoot;

    private void Start()
    {
        nextTimeToShoot = Time.time;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            Shoot();
    }

    private void Shoot()
    {
        if (nextTimeToShoot <= Time.time)
        {
            nextTimeToShoot = Time.time + 1f / fireRate;

            Quaternion spawnRotation = Quaternion.Euler(0f, spawnPoint.rotation.y, 0f);
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnRotation);

            bullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * bulletForce, ForceMode.Impulse);

            Destroy(bullet, bulletLifeTime);
        }
    }
}
