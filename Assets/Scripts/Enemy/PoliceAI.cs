using UnityEngine;

public class PoliceAI : EnemyAI
{
    [SerializeField] private float suspicionRange = 20f;
    [SerializeField] private int threatLevel = 1;
    private float suspicion = 0f;

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();
        UpdateSuspicion();
    }

    private void UpdateSuspicion()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerObj.transform.position);

        if (distanceToPlayer < suspicionRange)
        {
            suspicion += Time.deltaTime * 0.5f;
        }
        else
        {
            suspicion = Mathf.Max(0, suspicion - Time.deltaTime * 0.3f);
        }

        if (suspicion > 50f)
        {
            // Polícia vai atacar
            threatLevel = 2;
        }
    }

    public int GetThreatLevel() => threatLevel;
}