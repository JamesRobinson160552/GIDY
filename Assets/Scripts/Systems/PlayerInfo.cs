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
        nextLevelThreshold = Mathf.Pow((playerLevel/expRequirementModifier), expRequirementGrowthRate);
        levelText.text = playerLevel.ToString();

        SetExpBar();
    }

    private void SetExpBar()
    {
        expBar.minValue = 0;
        expBar.maxValue = nextLevelThreshold;
        expBar.value = exp;
    }
}
