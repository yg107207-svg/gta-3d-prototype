using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private Light sunLight;
    [SerializeField] private float dayDuration = 300f; // 5 minutos = 1 dia completo
    [SerializeField] private Color dayColor = Color.white;
    [SerializeField] private Color nightColor = new Color(0.3f, 0.3f, 0.5f);
    [SerializeField] private float dayIntensity = 1.5f;
    [SerializeField] private float nightIntensity = 0.3f;

    private float currentTime = 0f;
    private float sunRotation = 0f;

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= dayDuration)
            currentTime = 0f;

        UpdateSunPosition();
        UpdateLighting();
    }

    private void UpdateSunPosition()
    {
        // Calcula a rotação do sol (0 a 180 graus em um ciclo de dia)
        sunRotation = (currentTime / dayDuration) * 180f;
        sunLight.transform.rotation = Quaternion.Euler(sunRotation - 90f, 0, 0);
    }

    private void UpdateLighting()
    {
        // Transição suave entre dia e noite
        float timeRatio = currentTime / dayDuration;
        
        // Dia: 0.25 a 0.75, Noite: 0.75 a 1.0 e 0.0 a 0.25
        float intensity;
        Color color;

        if (timeRatio < 0.25f || timeRatio > 0.75f)
        {
            // Noite
            intensity = nightIntensity;
            color = nightColor;
        }
        else if (timeRatio < 0.5f)
        {
            // Amanhecer
            float t = (timeRatio - 0.25f) / 0.25f;
            intensity = Mathf.Lerp(nightIntensity, dayIntensity, t);
            color = Color.Lerp(nightColor, dayColor, t);
        }
        else
        {
            // Entardecer
            float t = (timeRatio - 0.5f) / 0.25f;
            intensity = Mathf.Lerp(dayIntensity, nightIntensity, t);
            color = Color.Lerp(dayColor, nightColor, t);
        }

        sunLight.intensity = intensity;
        sunLight.color = color;
    }

    public float GetTimeOfDay() => currentTime / dayDuration; // 0-1
    public bool IsNight() => GetTimeOfDay() > 0.75f || GetTimeOfDay() < 0.25f;
}