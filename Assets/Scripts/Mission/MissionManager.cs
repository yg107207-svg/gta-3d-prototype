using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private Mission[] availableMissions;
    [SerializeField] private Text missionDisplayText;
    private Mission currentMission;
    private PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if (currentMission != null && currentMission.IsActive())
        {
            currentMission.UpdateMission();
        }

        // Tecla M para abrir menu de missões
        if (Input.GetKeyDown(KeyCode.M))
        {
            DisplayAvailableMissions();
        }
    }

    public void AcceptMission(Mission mission)
    {
        if (currentMission != null && currentMission.IsActive())
        {
            currentMission.FailMission();
        }

        currentMission = mission;
        currentMission.StartMission();
        Debug.Log($"Missão aceita: {mission.missionName}");
    }

    public void CompleteMissionReward()
    {
        if (currentMission != null && currentMission.status == Mission.MissionStatus.Completed)
        {
            playerInventory.AddMoney(currentMission.reward);
            playerInventory.GetComponent<PlayerStats>().GainExperience(currentMission.reward / 2);
        }
    }

    private void DisplayAvailableMissions()
    {
        string missionsText = "=== MISSÕES DISPONÍVEIS ===\n";
        for (int i = 0; i < availableMissions.Length; i++)
        {
            missionsText += $"{i + 1}. {availableMissions[i].missionName} - ${availableMissions[i].reward}\n";
        }
        Debug.Log(missionsText);
    }

    public Mission GetCurrentMission() => currentMission;
}