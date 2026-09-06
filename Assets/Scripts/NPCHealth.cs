using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    float health;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f) Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
