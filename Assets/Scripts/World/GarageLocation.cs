using UnityEngine;
using UnityEngine.UI;

public class GarageLocation : LocationMarker
{
    [SerializeField] private GameObject[] vehicleSpawns;
    [SerializeField] private float spawnCost = 500f;

    void Start()
    {
        base.Start();
        locationName = "Garagem";
    }

    public void SpawnVehicle(int vehicleIndex)
    {
        if (vehicleIndex < 0 || vehicleIndex >= vehicleSpawns.Length)
            return;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        PlayerInventory inventory = playerObj.GetComponent<PlayerInventory>();

        if (inventory.SpendMoney(spawnCost))
        {
            Instantiate(vehicleSpawns[vehicleIndex], transform.position + Vector3.forward * 5f, Quaternion.identity);
            Debug.Log("Veículo spawned!");
        }
    }
}