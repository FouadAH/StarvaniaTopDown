using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;

    public CharacterMovement characterMovement { get; private set; }

    public IdleStateData IdleStateData;
    public PatrolStateData PatrolStateData;

    public IdleState idleState;
    public ChaseState chaseState;
    public PatrolState patrolState;

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

        stateMachine.Initialize(idleState);
    }

    public virtual void Update()
    {
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

    public void SetSpawner(EntitySpawner entitySpawner)
    {
        this.entitySpawner = entitySpawner;
    }

    [ContextMenu("TEST FIRE")]
    public void FireProjectile()
    {
        projectileController.FireProjectile(characterMovement.orientation.rotation);
    }

}