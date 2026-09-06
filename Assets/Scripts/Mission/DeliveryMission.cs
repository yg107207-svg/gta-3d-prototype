using UnityEngine;

public class DeliveryMission : Mission
{
    [SerializeField] private GameObject packagePrefab;
    [SerializeField] private Transform pickupLocation;
    [SerializeField] private Transform deliveryLocation;
    
    private GameObject currentPackage;
    private bool hasPackage = false;
    private bool nearPickup = false;
    private bool nearDelivery = false;

    void Start()
    {
        missionType = MissionType.Delivery;
        missionName = "Entregar Pacote";
        description = "Pégue o pacote no local marcado e entregue no destino";
        reward = 750f;
        timeLimit = 180f; // 3 minutos
    }

    void Update()
    {
        if (!isActive) return;

        UpdateMission();
        UpdateObjectiveUI($"Tempo: {timeRemaining:F0}s");

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        // Verificar presença no pickup
        if (!hasPackage && Vector3.Distance(player.transform.position, pickupLocation.position) < 3f)
        {
            nearPickup = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickupPackage();
            }
        }
        else
            nearPickup = false;

        // Verificar presença na entrega
        if (hasPackage && Vector3.Distance(player.transform.position, deliveryLocation.position) < 3f)
        {
            nearDelivery = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                DeliverPackage();
            }
        }
        else
            nearDelivery = false;
    }

    private void PickupPackage()
    {
        hasPackage = true;
        Debug.Log("Pacote coletado! Entregue no local marcado.");
        UpdateObjectiveUI($"Entregue o pacote em: {deliveryLocation.name}");
    }

    private void DeliverPackage()
    {
        CompleteMission();
    }
}