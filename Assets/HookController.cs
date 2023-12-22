using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    public LayerMask hookable;
    public float hookMaxDistance;
    public float pullSpeed;
    public Transform orientation;
    public LineRenderer lineRenderer;
    public PlayerDataSO playerData;
    public float manaPerHook;
    Entity hookedEntity;

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
        }
    }

    public void LaunchHook()
    {
        if (playerData.mana < manaPerHook)
            return;

        playerData.mana -= manaPerHook;

        var hit = Physics2D.Raycast(transform.position, orientation.transform.up, hookMaxDistance, hookable);
        if(hit.collider != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.collider.transform.position);

            hookedEntity = hit.collider.GetComponent<Entity>();

            if (hookedEntity != null)
            {
                hookedEntity.stateMachine.ChangeState(hookedEntity.pulledState);
            }
        }
    }
}
