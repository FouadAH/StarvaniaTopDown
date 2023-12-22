using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public PlayerDataSO playerData;
    public PlayerRuntimeDataSO runtimeData;
    private CharacterMovement characterMovement;

    private void Start()
    {
        playerData.health = playerData.maxHealth;
        characterMovement= GetComponent<CharacterMovement>();
    }

    public void Update()
    {
        playerData.mana += playerData.manaRegenRate * Time.deltaTime;
        playerData.mana = Mathf.Clamp(playerData.mana, 0, playerData.maxMana);

        runtimeData.playerRuntimePosition = transform.position;
    }

    public void TakeDamage(float damageAmount, Vector2 damageDirection)
    {
        Vector2 directionToPlayer = (Vector2)transform.position - damageDirection;
        characterMovement.KnockBack(directionToPlayer);

        playerData.health -= damageAmount;
        CheckDeath();
    }

    public void CheckDeath()
    {
        if(playerData.health < 0)
        {
            Destroy(gameObject);
        }
    }

    public void ComsumeMana(float amount)
    {
        playerData.mana -= amount;
    }
}
