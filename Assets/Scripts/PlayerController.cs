using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerDataSO playerData;

    [ContextMenu("Damage Player")]
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
}
