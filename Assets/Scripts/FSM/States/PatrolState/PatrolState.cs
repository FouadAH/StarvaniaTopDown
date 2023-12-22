using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PatrolState : State
{
    private PatrolStateData stateData;
    private Vector2 targetPosition;

    public PatrolState(Entity entity, FiniteStateMachine stateMachine, PatrolStateData stateData) : base(entity, stateMachine)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        targetPosition = (Vector3)entity.spawnPosition + Random.insideUnitSphere * stateData.patrolRange;
    }

    public override void Exit()
    { 
        base.Exit();
        entity.characterMovement.SetMovementDirection(Vector2.zero);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        float distanceToTarget = Vector2.Distance(entity.transform.position, targetPosition);

        if (entity.IsDetectingWall())
        {
            entity.stateMachine.ChangeState(entity.idleState);
            return;
        }

        if (distanceToTarget <= stateData.stoppingDistance)
        {
            entity.stateMachine.ChangeState(entity.idleState);
        }
        else if (entity.PlayerWithinRange_Min())
        {
            entity.stateMachine.ChangeState(entity.playerDetectedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Vector2 moveDirection = targetPosition - (Vector2)entity.transform.position;
        entity.characterMovement.SetMovementDirection(moveDirection.normalized);
    }
}
