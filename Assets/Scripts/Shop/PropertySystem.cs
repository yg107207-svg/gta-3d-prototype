using UnityEngine;
using UnityEngine.UI;

public class PropertySystem : MonoBehaviour
{
    [System.Serializable]
    public class Property
    {
        public string propertyName;
        public float purchasePrice;
        public float rentIncome = 0f;
        public Vector3 location;
        public bool isOwned = false;
        public GameObject propertyObject;
    }

    [SerializeField] private Property[] properties;
    [SerializeField] private Canvas propertyUI;
    [SerializeField] private Text propertyNameText;
    [SerializeField] private Text propertyPriceText;
    [SerializeField] private Button buyPropertyButton;

    private PlayerInventory playerInventory;
    private Property selectedProperty;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            playerInventory = playerObj.GetComponent<PlayerInventory>();

        if (propertyUI != null)
            propertyUI.gameObject.SetActive(false);
    }

    void Update()
    {
        // Coletar renda passiva
        CollectRentIncome();
    }

    public void ShowPropertyInfo(int propertyIndex)
    {
        if (propertyIndex < 0 || propertyIndex >= properties.Length)
            return;

        selectedProperty = properties[propertyIndex];

        if (propertyUI != null)
        {
            propertyUI.gameObject.SetActive(true);
            propertyNameText.text = selectedProperty.propertyName;
            propertyPriceText.text = $"Preço: ${selectedProperty.purchasePrice}";

            buyPropertyButton.interactable = !selectedProperty.isOwned;
            buyPropertyButton.GetComponentInChildren<Text>().text = 
                selectedProperty.isOwned ? "JÁ POSSUÍDA" : "COMPRAR";
        }
    }

    public void PurchaseProperty()
    {
        if (selectedProperty == null || selectedProperty.isOwned || playerInventory == null)
            return;

        if (playerInventory.SpendMoney(selectedProperty.purchasePrice))
        {
            selectedProperty.isOwned = true;
            Debug.Log($"Propriedade comprada: {selectedProperty.propertyName}");
            ShowPropertyInfo(System.Array.IndexOf(properties, selectedProperty));
        }
        else
        {
            Debug.Log("Dinheiro insuficiente!");
        }
    }

    private void CollectRentIncome()
    {
        foreach (Property prop in properties)
        {
            if (prop.isOwned && prop.rentIncome > 0)
            {
                // Renda a cada 60 segundos
                if (Time.time % 60 < Time.deltaTime)
                {
                    playerInventory.AddMoney(prop.rentIncome);
                    Debug.Log($"Renda recebida de {prop.propertyName}: ${prop.rentIncome}");
                }
            }
        }
    }

    public Property[] GetOwnedProperties()
    {
        return System.Array.FindAll(properties, p => p.isOwned);
    }
}