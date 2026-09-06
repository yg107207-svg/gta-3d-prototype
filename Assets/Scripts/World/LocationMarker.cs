using UnityEngine;
using UnityEngine.UI;

public class LocationMarker : MonoBehaviour
{
    [SerializeField] private string locationName = "Local";
    [SerializeField] private WorldMap.PointOfInterestType poiType;
    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] private Canvas interactionUI;
    [SerializeField] private Text locationText;
    [SerializeField] private Button interactButton;

    private bool playerNearby = false;

    void Start()
    {
        if (interactionUI != null)
            interactionUI.gameObject.SetActive(false);
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        playerNearby = distance < interactionDistance;

        if (interactionUI != null)
            interactionUI.gameObject.SetActive(playerNearby);

        if (locationText != null)
            locationText.text = locationName;
    }

    public string GetLocationName() => locationName;
    public WorldMap.PointOfInterestType GetPOIType() => poiType;
    public bool IsPlayerNearby() => playerNearby;
}