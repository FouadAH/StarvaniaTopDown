using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public PlayerDataSO playerData;
    public PlayerRuntimeDataSO runtimeData;
    public LevelEventChannel levelEventChannel;
    private CharacterMovement characterMovement;
    private CinemachineImpulseSource impulseSource;

    private void Start()
    {
        playerData.health = playerData.maxHealth;
        playerData.mana = playerData.maxMana;

        characterMovement= GetComponent<CharacterMovement>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        levelEventChannel.OnPlayerRespawn += OnRespawn;
    }

    private void OnDestroy()
    {
        levelEventChannel.OnPlayerRespawn -= OnRespawn;
    }

    public void Update()
    {
        playerData.mana += playerData.manaRegenRate * Time.deltaTime;
        playerData.mana = Mathf.Clamp(playerData.mana, 0, playerData.maxMana);

        runtimeData.playerRuntimePosition = transform.position;
    }

    void OnRespawn(Vector2 spawnPoint)
    {
        playerData.health = playerData.maxHealth;
        playerData.mana = playerData.maxMana;

        transform.position = spawnPoint;   
    }

    public void TakeDamage(float damageAmount, Vector2 damageDirection)
    {
        Vector2 directionToPlayer = (Vector2)transform.position - damageDirection;
        characterMovement.KnockBack(directionToPlayer);

        playerData.health -= damageAmount;
        impulseSource.GenerateImpulse(0.5f);

        CheckDeath();
    }

    public void CheckDeath()
    {
        if(playerData.health < 0)
        {
            levelEventChannel.RaiseOnPlayerDeath();
        }
    }

    public void ComsumeMana(float amount)
    {
        playerData.mana -= amount;
    }
}
