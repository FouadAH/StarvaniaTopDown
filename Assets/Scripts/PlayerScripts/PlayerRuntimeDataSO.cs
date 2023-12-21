using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerRuntimeData", menuName = "PlayerData/NewPlayerRuntimeData")]
public class PlayerRuntimeDataSO : ScriptableObject
{
    public Vector2 playerRuntimePosition;
}
