using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public PlayerDataSO playerData;
    public Slider healthSlider;
    public Slider manaSlider;

    private void Start()
    {
        healthSlider.maxValue = playerData.maxHealth;
        manaSlider.maxValue = playerData.maxMana;
    }

    public void Update()
    {
        healthSlider.value = Mathf.Lerp(healthSlider.value, playerData.health, 0.1f);
        manaSlider.value = Mathf.Lerp(manaSlider.value, playerData.mana, 0.1f);
    }
}
