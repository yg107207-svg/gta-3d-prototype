using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 25f;

    void OnCollisionEnter(Collision collision)
    {
        var hp = collision.gameObject.GetComponent<NPCHealth>();
        if (hp != null)
        {
            hp.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
