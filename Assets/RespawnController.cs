using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public Transform spawnPoint;
    public LevelEventChannel levelEventChannel;

    private void Start()
    {
        levelEventChannel.OnPlayerDeath += RespawnPlayer;
    }

    private void OnDestroy()
    {
        levelEventChannel.OnPlayerDeath -= RespawnPlayer;
    }

    void RespawnPlayer()
    {
        levelEventChannel.RaiseOnPlayerRespawn(spawnPoint.position);
    }
}
