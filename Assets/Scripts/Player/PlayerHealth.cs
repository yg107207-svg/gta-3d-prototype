using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float maxArmor = 100f;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image armorBar;
    [SerializeField] private Text healthText;
    
    private float currentHealth;
    private float currentArmor;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        currentArmor = maxArmor;
        UpdateUI();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        float armorDamage = damage * 0.5f;
        float remainingDamage = damage - armorDamage;

        if (currentArmor > 0)
        {
            currentArmor -= armorDamage;
            if (currentArmor < 0)
            {
                remainingDamage += Mathf.Abs(currentArmor);
                currentArmor = 0;
            }
        }
        else
        {
            remainingDamage = damage;
        }

        currentHealth -= remainingDamage;
        if (currentHealth < 0) currentHealth = 0;

        UpdateUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateUI();
    }

    public void AddArmor(float amount)
    {
        currentArmor += amount;
        if (currentArmor > maxArmor) currentArmor = maxArmor;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (healthBar != null)
            healthBar.fillAmount = currentHealth / maxHealth;
        if (armorBar != null)
            armorBar.fillAmount = currentArmor / maxArmor;
        if (healthText != null)
            healthText.text = $"HP: {currentHealth:F0}/{maxHealth}";
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Jogador morreu!");
        // Aqui você pode adicionar animação de morte, respawn, etc.
        Destroy(gameObject, 2f);
    }

    public float GetHealthPercent() => currentHealth / maxHealth;
    public bool IsDead() => isDead;
}