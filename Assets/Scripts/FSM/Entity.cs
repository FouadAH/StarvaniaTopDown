using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class Entity : MonoBehaviour
{
    public float aggroRange;
    public LayerMask playerMask;
    public PlayerRuntimeDataSO runtimeData;

    public TMPro.TMP_Text debugText;

    public FiniteStateMachine stateMachine;

    public CharacterMovement characterMovement { get; private set; }

    public IdleStateData IdleStateData;
    public PatrolStateData PatrolStateData;
    public PlayerDetectedStateData PlayerDetectedStateData;

    public IdleState idleState;
    public ChaseState chaseState;
    public PatrolState patrolState;
    public PlayerDetectedState playerDetectedState;

    private float health;

    private EntitySpawner entitySpawner;
    private ProjectileController projectileController;

    public virtual void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
        projectileController = GetComponent<ProjectileController>();

        stateMachine = new FiniteStateMachine();
        idleState = new IdleState(this, stateMachine, IdleStateData);
        patrolState = new PatrolState(this, stateMachine, PatrolStateData);
        playerDetectedState = new PlayerDetectedState(this, stateMachine, PlayerDetectedStateData);

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

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

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

    public bool PlayerWithinRange()
    {
        return Physics2D.OverlapCircle(transform.position, aggroRange, playerMask);
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
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}