using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickupBase
{
    public PlayerDataSO playerData;
    public float healingAmount;
    public override void OnPickup()
    {
        base.OnPickup();

        playerData.health = Mathf.Clamp(playerData.health + healingAmount, 0, playerData.maxHealth);
        Destroy(gameObject);
    }
}
