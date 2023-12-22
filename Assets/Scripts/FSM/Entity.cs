using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class Entity : MonoBehaviour, IDamageable
{
    public float maxHealth;
    public float minAggroRange;
    public float maxAggroRange;

    public LayerMask playerMask;
    public LayerMask obstacleMask;

    public PlayerRuntimeDataSO runtimeData;

    [Header("Debug Text")]
    public TMPro.TMP_Text debugText;

    [Header("Checkers")]
    public Transform wallChecker;

    [Header("States")]
    public IdleStateData IdleStateData;
    public PatrolStateData PatrolStateData;
    public PlayerDetectedStateData PlayerDetectedStateData;
    public PulledStateData PulledStateData;

    public IdleState idleState;
    public PatrolState patrolState;
    public PlayerDetectedState playerDetectedState;
    public PulledState pulledState;

    private float health;

    private EntitySpawner entitySpawner;
    private ProjectileController projectileController;

    public FiniteStateMachine stateMachine;
    public CharacterMovement characterMovement { get; private set; }

    public Vector2 spawnPosition { get; private set; }

    public virtual void Start()
    {
        health = maxHealth;
        spawnPosition = transform.position;

        characterMovement = GetComponent<CharacterMovement>();
        projectileController = GetComponent<ProjectileController>();

        stateMachine = new FiniteStateMachine();
        idleState = new IdleState(this, stateMachine, IdleStateData);
        patrolState = new PatrolState(this, stateMachine, PatrolStateData);
        playerDetectedState = new PlayerDetectedState(this, stateMachine, PlayerDetectedStateData);
        pulledState = new PulledState(this, stateMachine, PulledStateData);

        stateMachine.Initialize(idleState);
    }

    public virtual void Update()
    {
        debugText.text = stateMachine.currentState.ToString();

        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public void TakeDamage(float damageAmount, Vector2 damageDirection)
    {
        health -= damageAmount;

        Vector2 directionToPlayer = (Vector2)transform.position - damageDirection;
        characterMovement.KnockBack(directionToPlayer);

        if (health <= 0)
        {
            if (entitySpawner != null)
            {
                entitySpawner.Despawn(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public bool PlayerWithinRange_Min()
    {
        return Physics2D.OverlapCircle(transform.position, minAggroRange, playerMask);
    }
    public bool PlayerWithinRange_Max()
    {
        return Physics2D.OverlapCircle(transform.position, maxAggroRange, playerMask);
    }

    public bool IsDetectingWall()
    {
        return Physics2D.Linecast(transform.position, wallChecker.position, obstacleMask);
        //return Physics2D.OverlapCircle(wallChecker.transform.position, wallChecker.radius, obstacleMask);
    }

    public void SetSpawner(EntitySpawner entitySpawner)
    {
        this.entitySpawner = entitySpawner;
    }

    public void FireProjectile()
    {
        projectileController.FireProjectile(characterMovement.orientation.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minAggroRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxAggroRange);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, wallChecker.position);
    }
}