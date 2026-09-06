using UnityEngine;
using UnityEngine.UI;

public class WantedSystem : MonoBehaviour
{
    [SerializeField] private Text wantedText;
    [SerializeField] private Image wantedStars;
    [SerializeField] private float wantedDecayRate = 0.1f;

    private float wantedLevel = 0f;
    private float maxWantedLevel = 5f;
    private PoliceAI[] policeForces;

    void Start()
    {
        policeForces = FindObjectsOfType<PoliceAI>();
    }

    void Update()
    {
        if (wantedLevel > 0)
        {
            wantedLevel -= wantedDecayRate * Time.deltaTime;
            wantedLevel = Mathf.Clamp(wantedLevel, 0, maxWantedLevel);
        }

        UpdateUI();
        UpdatePoliceResponse();
    }

    public void AddWantedLevel(float amount)
    {
        wantedLevel += amount;
        wantedLevel = Mathf.Clamp(wantedLevel, 0, maxWantedLevel);
        Debug.Log($"Queria atualizado: {wantedLevel}");
    }

    private void UpdateUI()
    {
        if (wantedText != null)
            wantedText.text = $"Queria: {Mathf.CeilToInt(wantedLevel)}";

        if (wantedStars != null)
        {
            float alpha = (wantedLevel / maxWantedLevel);
            wantedStars.color = new Color(1, 0, 0, alpha);
        }
    }

    private void UpdatePoliceResponse()
    {
        foreach (PoliceAI police in policeForces)
        {
            if (wantedLevel > 1)
            {
                // Ativa polícia
            }
        }
    }

    public float GetWantedLevel() => wantedLevel;
}