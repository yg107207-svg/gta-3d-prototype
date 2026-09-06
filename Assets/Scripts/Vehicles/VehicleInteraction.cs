using UnityEngine;

public class VehicleInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 3f;
    private VehicleBase nearbyVehicle;
    private PlayerController playerController;
    private bool isInVehicle = false;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isInVehicle)
            {
                ExitVehicle();
            }
            else
            {
                TryEnterVehicle();
            }
        }

        // Detectar veículo próximo
        DetectNearbyVehicles();
    }

    private void DetectNearbyVehicles()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionDistance);
        nearbyVehicle = null;

        foreach (Collider col in colliders)
        {
            VehicleBase vehicle = col.GetComponent<VehicleBase>();
            if (vehicle != null)
            {
                nearbyVehicle = vehicle;
                break;
            }
        }
    }

    private void TryEnterVehicle()
    {
        if (nearbyVehicle != null)
        {
            nearbyVehicle.EnterVehicle(gameObject, null);
            isInVehicle = true;
            playerController.enabled = false;
        }
    }

    public void ExitVehicle()
    {
        if (nearbyVehicle != null && nearbyVehicle.IsPlayerInside())
        {
            nearbyVehicle.ExitVehicle();
            isInVehicle = false;
            playerController.enabled = true;
        }
    }

    public bool IsInVehicle() => isInVehicle;
}