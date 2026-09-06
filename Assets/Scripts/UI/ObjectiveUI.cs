using UnityEngine;
using UnityEngine.UI;

public class ObjectiveUI : MonoBehaviour
{
    [SerializeField] private Canvas objectiveCanvas;
    [SerializeField] private Text objectiveTitle;
    [SerializeField] private Text objectiveDescription;
    [SerializeField] private Image objectiveIcon;

    private MissionManager missionManager;

    void Start()
    {
        missionManager = FindObjectOfType<MissionManager>();
    }

    void Update()
    {
        if (missionManager != null)
        {
            Mission currentMission = missionManager.GetCurrentMission();
            if (currentMission != null && currentMission.IsActive())
            {
                objectiveCanvas.gameObject.SetActive(true);
                objectiveTitle.text = currentMission.missionName;
                objectiveDescription.text = currentMission.description;
            }
            else
            {
                objectiveCanvas.gameObject.SetActive(false);
            }
        }
    }
}