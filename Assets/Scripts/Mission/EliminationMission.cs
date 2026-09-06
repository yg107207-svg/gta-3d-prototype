using UnityEngine;

public class EliminationMission : Mission
{
    [SerializeField] private EnemyAI[] targetEnemies;
    private int enemiesEliminated = 0;
    private int totalEnemies;

    void Start()
    {
        missionType = MissionType.Elimination;
        missionName = "Eliminar Inimigos";
        description = "Elimine todos os inimigos marcados";
        reward = 1000f;
        timeLimit = 300f;
        totalEnemies = targetEnemies.Length;
    }

    void Update()
    {
        if (!isActive) return;

        UpdateMission();
        CheckEnemyStatus();
        UpdateObjectiveUI($"Inimigos: {totalEnemies - enemiesEliminated}/{totalEnemies}");
    }

    private void CheckEnemyStatus()
    {
        foreach (EnemyAI enemy in targetEnemies)
        {
            if (enemy == null || enemy.GetComponent<EnemyHealth>().IsDead())
            {
                if (!enemy.GetComponent<EnemyHealth>().IsDead())
                {
                    enemiesEliminated++;
                }
            }
        }

        if (enemiesEliminated >= totalEnemies)
        {
            CompleteMission();
        }
    }
}