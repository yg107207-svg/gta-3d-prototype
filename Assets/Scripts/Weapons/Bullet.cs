using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f;
    private float damage;
    private float speed;
    private float range;
    private Vector3 direction;
    private GameObject shooter;
    private float distanceTraveled = 0f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        distanceTraveled += speed * Time.fixedDeltaTime;
        if (distanceTraveled > range)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(float dmg, float spd, float rng, Vector3 dir, GameObject shoot)
    {
        damage = dmg;
        speed = spd;
        range = rng;
        direction = dir.normalized;
        shooter = shoot;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == shooter) return; // Não dar dano em si mesmo

        NPCHealth npcHealth = collision.gameObject.GetComponent<NPCHealth>();
        if (npcHealth != null)
        {
            npcHealth.TakeDamage(damage);
        }

        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}