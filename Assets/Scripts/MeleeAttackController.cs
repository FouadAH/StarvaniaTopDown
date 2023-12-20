using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackController : MonoBehaviour
{
    public PlayerDataSO playerData;
    private float currentAttackTime;

    private bool canAttack = true;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        currentAttackTime += Time.deltaTime;
        canAttack = currentAttackTime >= playerData.attackCooldown;
    }

    public void OnAttack()
    {
        if(!canAttack)
            return;

        currentAttackTime = 0;
        animator.Play("SwordAttack");
    }
}
