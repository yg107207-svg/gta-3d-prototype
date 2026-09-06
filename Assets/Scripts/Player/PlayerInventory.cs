using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [System.Serializable]
    public class WeaponSlot
    {
        public WeaponType weaponType;
        public int ammo = 0;
        public bool isUnlocked = false;
    }

    public enum WeaponType { Pistol, Submachine, Shotgun, Sniper, Grenade }

    [SerializeField] private WeaponSlot[] weapons = new WeaponSlot[5];
    [SerializeField] private int currentWeaponIndex = 0;
    [SerializeField] private Text ammoText;
    [SerializeField] private Text moneyText;

    private float money = 1000f;
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        InitializeWeapons();
        UpdateUI();
    }

    void Update()
    {
        // Trocar armas com números
        for (int i = 0; i < weapons.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.E + i) && weapons[i].isUnlocked)
            {
                SelectWeapon(i);
            }
        }

        // Scroll do mouse para trocar armas
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            SelectWeapon((currentWeaponIndex + 1) % weapons.Length);
        }
        else if (scroll < 0f)
        {
            SelectWeapon((currentWeaponIndex - 1 + weapons.Length) % weapons.Length);
        }
    }

    private void InitializeWeapons()
    {
        weapons[0] = new WeaponSlot { weaponType = WeaponType.Pistol, ammo = 120, isUnlocked = true };
        weapons[1] = new WeaponSlot { weaponType = WeaponType.Submachine, ammo = 0, isUnlocked = false };
        weapons[2] = new WeaponSlot { weaponType = WeaponType.Shotgun, ammo = 0, isUnlocked = false };
        weapons[3] = new WeaponSlot { weaponType = WeaponType.Sniper, ammo = 0, isUnlocked = false };
        weapons[4] = new WeaponSlot { weaponType = WeaponType.Grenade, ammo = 0, isUnlocked = false };
    }

    public void SelectWeapon(int index)
    {
        if (index >= 0 && index < weapons.Length && weapons[index].isUnlocked)
        {
            currentWeaponIndex = index;
            playerController.EquipWeapon(index);
            UpdateUI();
        }
    }

    public void AddAmmo(WeaponType type, int amount)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].weaponType == type)
            {
                weapons[i].ammo += amount;
                UpdateUI();
                return;
            }
        }
    }

    public bool UseAmmo(int amount)
    {
        if (weapons[currentWeaponIndex].ammo >= amount)
        {
            weapons[currentWeaponIndex].ammo -= amount;
            UpdateUI();
            return true;
        }
        return false;
    }

    public void AddMoney(float amount)
    {
        money += amount;
        UpdateUI();
    }

    public bool SpendMoney(float amount)
    {
        if (money >= amount)
        {
            money -= amount;
            UpdateUI();
            return true;
        }
        return false;
    }

    private void UpdateUI()
    {
        if (ammoText != null)
            ammoText.text = $"Ammo: {weapons[currentWeaponIndex].ammo}";
        if (moneyText != null)
            moneyText.text = $"${money:F0}";
    }

    public float GetMoney() => money;
    public int GetCurrentAmmo() => weapons[currentWeaponIndex].ammo;
    public WeaponType GetCurrentWeapon() => weapons[currentWeaponIndex].weaponType;
}