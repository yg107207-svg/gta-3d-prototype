using UnityEngine;
using UnityEngine.UI;

public class AmmoShop : MonoBehaviour
{
    [SerializeField] private float ammoPricePerMag = 50f;
    [SerializeField] private int ammoPerMag = 30;
    [SerializeField] private Canvas shopUI;
    [SerializeField] private Button buyButton;
    [SerializeField] private Text ammoText;

    private PlayerInventory playerInventory;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            playerInventory = playerObj.GetComponent<PlayerInventory>();

        if (shopUI != null)
            shopUI.gameObject.SetActive(false);
    }

    public void OpenAmmoShop()
    {
        if (shopUI != null)
            shopUI.gameObject.SetActive(true);

        if (ammoText != null)
            ammoText.text = $"Munição: ${ammoPricePerMag} por {ammoPerMag} tiros";
    }

    public void BuyAmmo()
    {
        if (playerInventory == null)
            return;

        if (playerInventory.SpendMoney(ammoPricePerMag))
        {
            playerInventory.AddAmmo(PlayerInventory.WeaponType.Pistol, ammoPerMag);
            Debug.Log($"Comprou {ammoPerMag} munições");
        }
    }
}