using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{
    [System.Serializable]
    public enum MissionType { Delivery, Elimination, Rescue, Stealth, Racing }

    [System.Serializable]
    public enum MissionStatus { Available, Active, Completed, Failed }

    public MissionType missionType;
    public MissionStatus status = MissionStatus.Available;
    public string missionName = "Missão";
    public string description = "Descrição da missão";
    public float reward = 500f;
    public float timeLimit = 300f; // 5 minutos

    [SerializeField] protected Transform objectiveLocation;
    [SerializeField] protected Text objectiveText;

    protected float timeRemaining;
    protected bool isActive = false;

    public virtual void StartMission()
    {
        isActive = true;
        status = MissionStatus.Active;
        timeRemaining = timeLimit;
        Debug.Log($"Missão iniciada: {missionName}");
    }

    public virtual void UpdateMission()
    {
        if (!isActive) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            FailMission();
        }
    }

    public virtual void CompleteMission()
    {
        isActive = false;
        status = MissionStatus.Completed;
        Debug.Log($"Missão completada: {missionName}. Recompensa: ${reward}");
    }

    public virtual void FailMission()
    {
        isActive = false;
        status = MissionStatus.Failed;
        Debug.Log($"Missão falhou: {missionName}");
    }

    protected void UpdateObjectiveUI(string text)
    {
        if (objectiveText != null)
            objectiveText.text = text;
    }

    public float GetTimeRemaining() => timeRemaining;
    public bool IsActive() => isActive;
}