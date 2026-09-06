using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float strength = 1f;      // Dano
    [SerializeField] private float stamina = 1f;       // Velocidade/Resistência
    [SerializeField] private float defense = 1f;       // Redução de dano
    [SerializeField] private float precision = 1f;     // Acurácia

    private float totalExperience = 0f;
    private int level = 1;

    public void GainExperience(float amount)
    {
        totalExperience += amount;
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        int newLevel = Mathf.FloorToInt(totalExperience / 500f) + 1;
        if (newLevel > level)
        {
            level = newLevel;
            OnLevelUp();
        }
    }

    private void OnLevelUp()
    {
        strength *= 1.05f;
        stamina *= 1.05f;
        defense *= 1.05f;
        precision *= 1.05f;
        Debug.Log($"Level Up! Novo nível: {level}");
    }

    public float GetDamageMultiplier() => strength;
    public float GetSpeedMultiplier() => stamina;
    public float GetDefenseMultiplier() => defense;
    public float GetPrecisionMultiplier() => precision;
    public int GetLevel() => level;
}