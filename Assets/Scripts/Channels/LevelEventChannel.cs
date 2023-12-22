using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class LevelEventChannel : ScriptableObject
{
    public UnityAction OnKeyPickedUp;
    public UnityAction OnPlayerDeath;
    public UnityAction<Vector2> OnPlayerRespawn;


    public void RaiseOnKeyPickedUp()
    {
        OnKeyPickedUp?.Invoke();
    }

    public void RaiseOnPlayerDeath() 
    {  
        OnPlayerDeath?.Invoke(); 
    }

    public void RaiseOnPlayerRespawn(Vector2 spawnPos)
    {
        OnPlayerRespawn?.Invoke(spawnPos);
    }
}
