using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private Text healthText;
    [SerializeField] private Image healthBar;
    [SerializeField] private Text ammoText;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text wantedText;
    [SerializeField] private Text weaponText;
    [SerializeField] private Text locationText;
    [SerializeField] private Text missionText;
    [SerializeField] private Image minimap;

    private PlayerHealth playerHealth;
    private PlayerInventory playerInventory;
    private WantedSystem wantedSystem;
    private MissionManager missionManager;
    private WorldMap worldMap;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerHealth = playerObj.GetComponent<PlayerHealth>();
            playerInventory = playerObj.GetComponent<PlayerInventory>();
        }

        wantedSystem = FindObjectOfType<WantedSystem>();
        missionManager = FindObjectOfType<MissionManager>();
        worldMap = FindObjectOfType<WorldMap>();
    }

    void Update()
    {
        UpdateHealthDisplay();
        UpdateAmmoDisplay();
        UpdateMoneyDisplay();
        UpdateWantedDisplay();
        UpdateWeaponDisplay();
        UpdateMissionDisplay();
    }

    private void UpdateHealthDisplay()
    {
        if (playerHealth != null)
        {
            float healthPercent = playerHealth.GetHealthPercent();
            if (healthBar != null)
                healthBar.fillAmount = healthPercent;
            if (healthText != null)
                healthText.text = $"HP: {healthPercent * 100:F0}%";
        }
    }

    private void UpdateAmmoDisplay()
    {
        if (playerInventory != null)
        {
            if (ammoText != null)
                ammoText.text = $"Munição: {playerInventory.GetCurrentAmmo()}";
        }
    }

    private void UpdateMoneyDisplay()
    {
        if (playerInventory != null)
        {
            if (moneyText != null)
                moneyText.text = $"${playerInventory.GetMoney():F0}";
        }
    }

    private void UpdateWantedDisplay()
    {
        if (wantedSystem != null)
        {
            if (wantedText != null)
                wantedText.text = $"Queria: {Mathf.CeilToInt(wantedSystem.GetWantedLevel())}";
        }
    }

    private void UpdateWeaponDisplay()
    {
        if (playerInventory != null)
        {
            if (weaponText != null)
                weaponText.text = playerInventory.GetCurrentWeapon().ToString();
        }
    }

    private void UpdateMissionDisplay()
    {
        if (missionManager != null)
        {
            Mission currentMission = missionManager.GetCurrentMission();
            if (currentMission != null && currentMission.IsActive())
            {
                if (missionText != null)
                    missionText.text = $"Missão: {currentMission.missionName}";
            }
        }
    }
}