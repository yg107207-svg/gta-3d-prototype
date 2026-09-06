using UnityEngine;
using UnityEngine.UI;

public class HospitalLocation : LocationMarker
{
    [SerializeField] private float healCost = 100f;
    [SerializeField] private float healAmount = 100f;

    void Start()
    {
        base.Start();
        locationName = "Hospital";
    }

    public void HealPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null) return;

        PlayerHealth playerHealth = playerObj.GetComponent<PlayerHealth>();
        PlayerInventory inventory = playerObj.GetComponent<PlayerInventory>();

        if (playerHealth != null && inventory != null)
        {
            if (inventory.SpendMoney(healCost))
            {
                playerHealth.Heal(healAmount);
                Debug.Log($"Curado! Gastou ${healCost}");
            }
            else
            {
                Debug.Log("Dinheiro insuficiente!");
            }
        }
    }
}