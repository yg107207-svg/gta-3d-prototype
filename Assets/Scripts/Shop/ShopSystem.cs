using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    [System.Serializable]
    public class ShopItem
    {
        public string itemName;
        public float price;
        public ItemType type;
        public int quantity = 1;
    }

    public enum ItemType { Weapon, Ammo, Armor, Health, Vehicle }

    [SerializeField] private ShopItem[] shopItems;
    [SerializeField] private Canvas shopUI;
    [SerializeField] private Transform itemContainer;
    [SerializeField] private Button buyButton;
    [SerializeField] private Text priceText;
    [SerializeField] private Text moneyText;

    private PlayerInventory playerInventory;
    private ShopItem selectedItem;
    private bool shopOpen = false;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            playerInventory = playerObj.GetComponent<PlayerInventory>();

        if (shopUI != null)
            shopUI.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && shopOpen)
        {
            ToggleShop();
        }
    }

    public void OpenShop()
    {
        shopOpen = true;
        if (shopUI != null)
            shopUI.gameObject.SetActive(true);
        DisplayItems();
    }

    public void CloseShop()
    {
        shopOpen = false;
        if (shopUI != null)
            shopUI.gameObject.SetActive(false);
    }

    private void ToggleShop()
    {
        if (shopOpen)
            CloseShop();
        else
            OpenShop();
    }

    private void DisplayItems()
    {
        // Limpar itens anteriores
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        // Criar botões para cada item
        foreach (ShopItem item in shopItems)
        {
            Button itemButton = Instantiate(buyButton, itemContainer);
            itemButton.GetComponentInChildren<Text>().text = $"{item.itemName} - ${item.price}";
            itemButton.onClick.AddListener(() => SelectItem(item));
        }
    }

    private void SelectItem(ShopItem item)
    {
        selectedItem = item;
        if (priceText != null)
            priceText.text = $"Preço: ${item.price}";
    }

    public void BuyItem()
    {
        if (selectedItem == null || playerInventory == null)
            return;

        if (playerInventory.SpendMoney(selectedItem.price))
        {
            ApplyItemPurchase(selectedItem);
            Debug.Log($"Comprado: {selectedItem.itemName}");
            UpdateMoneyDisplay();
        }
        else
        {
            Debug.Log("Dinheiro insuficiente!");
        }
    }

    private void ApplyItemPurchase(ShopItem item)
    {
        switch (item.type)
        {
            case ItemType.Ammo:
                playerInventory.AddAmmo(PlayerInventory.WeaponType.Pistol, 30);
                break;
            case ItemType.Armor:
                PlayerHealth playerHealth = playerInventory.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                    playerHealth.AddArmor(50f);
                break;
            case ItemType.Health:
                playerHealth = playerInventory.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                    playerHealth.Heal(50f);
                break;
        }
    }

    private void UpdateMoneyDisplay()
    {
        if (moneyText != null && playerInventory != null)
            moneyText.text = $"${playerInventory.GetMoney():F0}";
    }
}