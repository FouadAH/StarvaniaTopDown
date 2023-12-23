using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HookController : MonoBehaviour
{
    public LayerMask hookable;
    public LayerMask obstacles;

    public float hookMaxDistance;
    public float pullSpeed;
    public Transform orientation;
    public LineRenderer lineRenderer;
    public PlayerDataSO playerData;
    public float manaPerHook;
    Entity hookedEntity;
    bool isPulling;

    private void Update()
    {
        if (hookedEntity == null)
            return;

        if(hookedEntity.stateMachine.currentState is PulledState)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hookedEntity.transform.position);
        }
        else
        {
            lineRenderer.enabled = false;
            hookedEntity = null;
            isPulling = false;
        }
    }

    public void LaunchHook()
    {
        if (playerData.mana < manaPerHook)
            return;

        if (isPulling)
            return;

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionToMouse = mousePosition - transform.position;

        var hit = Physics2D.Raycast(transform.position, directionToMouse, hookMaxDistance, hookable);
        bool hitObstacle = Physics2D.Raycast(transform.position, directionToMouse, hit.distance, obstacles);

        if (hit.collider != null && !hitObstacle)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.collider.transform.position);

            hookedEntity = hit.collider.GetComponent<Entity>();

            if (hookedEntity != null)
            {
                isPulling = true;
                playerData.mana -= manaPerHook;
                hookedEntity.stateMachine.ChangeState(hookedEntity.pulledState);
            }
        }
    }
}
