using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private float expRequirementModifier;
    [SerializeField] private float expRequirementGrowthRate;
    public int playerLevel;
    public int exp;
    public float nextLevelThreshold;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Slider expBar;

    public void Awake()
    {
        playerLevel = PlayerPrefs.GetInt("playerLevel", 1);
        exp = PlayerPrefs.GetInt("exp", 0);
        SetLevelThreshold();
        SetExpBar();
    }

    private void SetExpBar()
    {
        expBar.minValue = 0;
        expBar.maxValue = nextLevelThreshold;
        expBar.value = exp;
    }

    public void GainExp(int expToGain)
    {
        exp += expToGain;
        if (exp >= nextLevelThreshold)
        {
            playerLevel += 1;
            PlayerPrefs.SetInt("playerLevel", playerLevel);
            exp = Mathf.FloorToInt((float)exp - nextLevelThreshold);
            SetLevelThreshold();
            SetExpBar();
        }
        else
        {
            expBar.value = exp;
        }
    }

    public void SetLevelThreshold()
    {
        nextLevelThreshold = Mathf.Pow((playerLevel/expRequirementModifier), expRequirementGrowthRate);
        levelText.text = playerLevel.ToString();
    }
}
