using UnityEngine;

public class NPCPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float detectRange = 8f;

    private int index = 0;
    private Transform player;

    void Start()
    {
        var p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) < detectRange)
        {
            // perseguir
            transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            transform.LookAt(player);
            return;
        }

        if (waypoints == null || waypoints.Length == 0) return;

        Transform target = waypoints[index];
        transform.position = Vector3.MoveTowards(transform.position, target.position, patrolSpeed * Time.deltaTime);
        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            index = (index + 1) % waypoints.Length;
        }
    }
}
