using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerDataSO playerData;

    public void Update()
    {
        playerData.mana += playerData.manaRegenRate * Time.deltaTime;
        playerData.mana = Mathf.Clamp(playerData.mana, 0, playerData.maxMana); 
    }

    public void TakeDamage(float damageAmount)
    {
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

    [ContextMenu("Damage Player")]
    public void TestDamage()
    {
        TakeDamage(10f);
    }

    [ContextMenu("Consume Player Mana")]
    public void TestComsumeMana()
    {
        ComsumeMana(10f);
    }
}
