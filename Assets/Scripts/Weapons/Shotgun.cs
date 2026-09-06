using UnityEngine;

public class Shotgun : WeaponBase
{
    [SerializeField] private int pelletsPerShot = 8;

    protected override void Start()
    {
        base.Start();
        stats.weaponName = "Shotgun";
        stats.damage = 50f;
        stats.fireRate = 0.6f;
        stats.accuracy = 0.7f;
        stats.magCapacity = 8;
        stats.bulletSpeed = 40f;
    }

    protected override void SpawnBullet()
    {
        for (int i = 0; i < pelletsPerShot; i++)
        {
            Vector3 spread = Random.insideUnitSphere * (1f - stats.accuracy);
            Vector3 bulletDirection = (muzzlePoint.forward + spread).normalized;

            GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, Quaternion.LookRotation(bulletDirection));
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.Initialize(stats.damage / pelletsPerShot, stats.bulletSpeed, stats.range, bulletDirection, gameObject);
            }
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = bulletDirection * stats.bulletSpeed;
            }
        }
    }
}
