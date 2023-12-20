using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData", menuName ="PlayerData/New Player Data")]
public class PlayerDataSO : ScriptableObject
{
    public float maxHealth;
    public float health;

    public float maxMana;
    public float mana;

    public float manaRegenRate;
    public float attackCooldown;
}
