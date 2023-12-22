using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public PlayerDataSO playerData;
    public LevelDataSO levelData;
    public LevelEventChannel levelEventChannel;

    public TMPro.TMP_Text keyCountText;
    public Slider healthSlider;
    public Slider manaSlider;

    private void Start()
    {
        healthSlider.maxValue = playerData.maxHealth;
        manaSlider.maxValue = playerData.maxMana;

        levelEventChannel.OnKeyPickedUp += OnPickedUpKey;

        InitKeyCountText();
    }

    private void OnDestroy()
    {
        levelEventChannel.OnKeyPickedUp -= OnPickedUpKey;
    }

    public void Update()
    {
        healthSlider.value = Mathf.Lerp(healthSlider.value, playerData.health, 0.1f);
        manaSlider.value = Mathf.Lerp(manaSlider.value, playerData.mana, 0.1f);
    }

    void OnPickedUpKey()
    {
        InitKeyCountText();
    }

    void InitKeyCountText()
    {
        int collectedKeysCount = 0;
        int totalKeysCount = levelData.levelKeys.Count;

        foreach (var key in levelData.levelKeys)
        {
            if (key.hasBeenCollected)
            {
                collectedKeysCount++;
            }
        }

        keyCountText.text = $"{collectedKeysCount}/{totalKeysCount}";
    }
}
