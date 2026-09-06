using UnityEngine;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    [System.Serializable]
    public enum AIState { Idle, Patrol, Chase, Attack, Investigate }

    [SerializeField] private AIState currentState = AIState.Patrol;
    [SerializeField] private float detectionRange = 15f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float chaseSpeed = 5f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private Transform[] patrolWaypoints;
    
    private Transform player;
    private int currentWaypointIndex = 0;
    private float lastAttackTime = -999f;
    private Vector3 lastKnownPlayerPosition;
    private CharacterController controller;
    private Animator animator;
    private bool canAttack = true;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Transições de estado
        if (distanceToPlayer < detectionRange)
        {
            lastKnownPlayerPosition = player.position;
            if (distanceToPlayer < attackRange)
            {
                ChangeState(AIState.Attack);
            }
            else
            {
                ChangeState(AIState.Chase);
            }
        }
        else if (currentState == AIState.Chase || currentState == AIState.Investigate)
        {
            ChangeState(AIState.Patrol);
        }

        // Executar lógica do estado atual
        switch (currentState)
        {
            case AIState.Patrol:
                Patrol();
                break;
            case AIState.Chase:
                Chase();
                break;
            case AIState.Attack:
                Attack();
                break;
            case AIState.Investigate:
                Investigate();
                break;
        }
    }

    private void Patrol()
    {
        if (patrolWaypoints == null || patrolWaypoints.Length == 0)
            return;

        Transform target = patrolWaypoints[currentWaypointIndex];
        Vector3 direction = (target.position - transform.position).normalized;
        
        if (controller != null)
            controller.Move(direction * patrolSpeed * Time.deltaTime);
        
        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
        }
    }

    private void Chase()
    {
        Vector3 direction = (lastKnownPlayerPosition - transform.position).normalized;
        
        if (controller != null)
            controller.Move(direction * chaseSpeed * Time.deltaTime);
        
        transform.LookAt(player);
    }

    private void Attack()
    {
        transform.LookAt(player);

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            // Aqui você pode adicionar lógica de ataque (tiro, melee, etc.)
            lastAttackTime = Time.time;
            PerformAttack();
        }
    }

    private void Investigate()
    {
        Vector3 direction = (lastKnownPlayerPosition - transform.position).normalized;
        
        if (controller != null)
            controller.Move(direction * patrolSpeed * Time.deltaTime);
        
        transform.LookAt(lastKnownPlayerPosition);
    }

    private void PerformAttack()
    {
        // Lógica de ataque - adicione som, partículas, etc.
        Debug.Log("Inimigo atacando!");
        
        // Se tiver componente de arma
        WeaponBase weapon = GetComponent<WeaponBase>();
        if (weapon != null)
        {
            weapon.Fire();
        }
    }

    private void ChangeState(AIState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    public AIState GetCurrentState() => currentState;
}