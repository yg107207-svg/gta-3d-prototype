using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [System.Serializable]
    public class WeaponStats
    {
        public string weaponName = "Weapon";
        public float damage = 25f;
        public float fireRate = 0.1f;
        public float accuracy = 0.95f;
        public float reloadTime = 2f;
        public int magCapacity = 30;
        public float bulletSpeed = 50f;
        public float range = 100f;
    }

    public WeaponStats stats = new WeaponStats();
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform muzzlePoint;

    protected float lastFireTime = -999f;
    protected int currentAmmo;
    protected bool isReloading = false;
    protected PlayerInventory inventory;

    protected virtual void Start()
    {
        currentAmmo = stats.magCapacity;
        inventory = GetComponent<PlayerInventory>();
    }

    public virtual void Fire()
    {
        if (isReloading || Time.time - lastFireTime < stats.fireRate)
            return;

        if (currentAmmo <= 0)
        {
            Reload();
            return;
        }

        lastFireTime = Time.time;
        currentAmmo--;

        if (muzzlePoint != null && bulletPrefab != null)
        {
            SpawnBullet();
        }
    }

    protected virtual void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.Initialize(stats.damage, stats.bulletSpeed, stats.range, muzzlePoint.forward, gameObject);
        }
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = muzzlePoint.forward * stats.bulletSpeed;
        }
    }

    public virtual void Reload()
    {
        if (isReloading) return;
        StartCoroutine(ReloadCoroutine());
    }

    protected System.Collections.IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(stats.reloadTime);
        currentAmmo = stats.magCapacity;
        isReloading = false;
    }

    public int GetCurrentAmmo() => currentAmmo;
    public int GetMagCapacity() => stats.magCapacity;
    public bool IsReloading() => isReloading;
}