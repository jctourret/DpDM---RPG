using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : Interactable
{
    PlayerManager playerManager;
    CharacterStats myStats;
    CharacterCombat combat;

    public NavMeshAgent agent;
    public Transform playerPos;
    public LayerMask WhatIsGround, WhatIsPlayer;

    float rotationspeed = 5f;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange = 5;
    public float walkPointInterval = 1.5f;
    public float walkPointCooldown = 0;

    public float sightRange = 5;
    public float attackRange = 2.5f;
    bool playerInSightRange, playerInAttackRange;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
        combat = GetComponent<CharacterCombat>();
        playerPos = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        WhatIsGround = LayerMask.GetMask("Ground");
        WhatIsPlayer = LayerMask.GetMask("Player");
    }
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);
        walkPointCooldown -= Time.deltaTime;
        if(!playerInAttackRange && !playerInSightRange)
        {
            Patrolling();
        }
        if(!playerInAttackRange && playerInSightRange)
        {
            ChasePlayer();
            FacePlayer();
        }
        if(playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
    }
    public override void Interact()
    {
        base.Interact();
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
        if(playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }
    }
    private void Patrolling()
    {
        if (walkPointCooldown < 0)
        {
            if (!walkPointSet)
            {
                SearchForWalkPoint();
            }
            else
            {
                agent.SetDestination(walkPoint);
            }
            Vector3 distanceToWalkPoint = transform.position - walkPoint;
            if (distanceToWalkPoint.magnitude < 1f)
            {
                walkPointSet = false;
            }
            walkPointCooldown = walkPointInterval;
        }
    }
    private void SearchForWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if(Physics.Raycast(walkPoint,-transform.up, 2f, WhatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(playerPos.position);

    }
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        FacePlayer();
        CharacterStats targetStats = playerManager.player.GetComponent<CharacterStats>();
        combat.Attack(targetStats);
    }
    private void FacePlayer()
    {
        Vector3 direction = (playerPos.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime * rotationspeed);
    }
}
