using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : PickupBase
{
    public KeySO keyData;
    public LevelEventChannel levelEventChannel;

    private void Awake()
    {
        keyData.hasBeenCollected = false;
    }

    public override void OnPickup()
    {
        base.OnPickup();
        if (keyData.hasBeenCollected)
            return;

        keyData.hasBeenCollected = true;
        levelEventChannel.RaiseOnKeyPickedUp();
        Destroy(gameObject);
    }
}
