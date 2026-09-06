using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireCooldown = 0.25f;
    public float projectileSpeed = 20f;

    float lastFireTime = -999f;

    public void Fire(Transform muzzle)
    {
        if (Time.time - lastFireTime < fireCooldown) return;
        lastFireTime = Time.time;

        if (projectilePrefab == null || muzzle == null) return;

        GameObject p = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
        Rigidbody rb = p.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = muzzle.forward * projectileSpeed;
        }

        Destroy(p, 5f);
    }
}
