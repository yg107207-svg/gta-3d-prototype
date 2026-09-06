using UnityEngine;
using UnityEngine.UI;

public class WorldMap : MonoBehaviour
{
    [System.Serializable]
    public class PointOfInterest
    {
        public string name;
        public Vector3 position;
        public Image icon;
        public PointOfInterestType type;
    }

    public enum PointOfInterestType { Mission, Store, Hospital, Police, Garage, Property }

    [SerializeField] private PointOfInterest[] pointsOfInterest;
    [SerializeField] private RawImage mapImage;
    [SerializeField] private Transform playerIcon;
    [SerializeField] private Vector3 mapCenter = Vector3.zero;
    [SerializeField] private float mapScale = 0.1f;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            UpdatePlayerIconOnMap();
        }
    }

    private void UpdatePlayerIconOnMap()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 mapPos = (playerPos - mapCenter) * mapScale;
        playerIcon.localPosition = new Vector3(mapPos.x, mapPos.z, 0);
    }

    public PointOfInterest FindNearestPOI(Vector3 playerPos)
    {
        PointOfInterest nearest = null;
        float minDistance = float.MaxValue;

        foreach (PointOfInterest poi in pointsOfInterest)
        {
            float distance = Vector3.Distance(playerPos, poi.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = poi;
            }
        }

        return nearest;
    }

    public PointOfInterest[] GetPOIByType(PointOfInterestType type)
    {
        return System.Array.FindAll(pointsOfInterest, poi => poi.type == type);
    }
}