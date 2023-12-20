using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public PlayerDataSO playerData;
    public Slider healthSlider;
    public Slider manaSlider;

    public void Update()
    {
        healthSlider.value = playerData.health / playerData.maxHealth;
        manaSlider.value = playerData.mana / playerData.maxMana;
    }
}
