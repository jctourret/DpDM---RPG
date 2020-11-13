using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : Interactable
{
    PlayerManager playerManager;
    CharacterStats myStats;
    CharacterCombat combat;
    Animator enemyAnim;

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
    public float attackRange = 2f;
    bool playerInSightRange, playerInAttackRange;
    bool spottedPlayer = false;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
        combat = GetComponent<CharacterCombat>();
        enemyAnim = GetComponent<Animator>();
        playerPos = PlayerManager.instance.transform;
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
        Animator playerAnim = PlayerController.instance.GetComponent<Animator>();
        playerAnim.SetTrigger("attack");
        CharacterCombat playerCombat = PlayerController.instance.GetComponent<CharacterCombat>();
        if(playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }
    }
    private void Patrolling()
    {
        enemyAnim.SetBool("walking",true);
        if (walkPointCooldown < 0)
        {
            enemyAnim.SetBool("walking", true);
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
        else
        {
            enemyAnim.SetBool("walking", false);
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
        if (spottedPlayer)
        {
            AudioManager.instance.Play("Breath");
        }
        spottedPlayer = true;
        enemyAnim.SetBool("walking", true);
        agent.SetDestination(playerPos.position);
    }
    private void AttackPlayer()
    {
        enemyAnim.SetBool("walking", false);
        agent.SetDestination(transform.position);
        FacePlayer();
        CharacterStats targetStats = PlayerController.instance.GetComponent<CharacterStats>();
        combat.Attack(targetStats);
        enemyAnim.SetBool("attack", true);
    }
    private void FacePlayer()
    {
        Vector3 direction = (playerPos.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime * rotationspeed);
    }
}
