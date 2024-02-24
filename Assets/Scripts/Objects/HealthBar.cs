using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI num;
    private int currentHealth;
    private int maxHealth;

    public void InitializeHealthBar(string name, int characterMaxHealth)
    {
        characterName.text = name;
        maxHealth = characterMaxHealth;
        currentHealth = characterMaxHealth;

        slider.maxValue = maxHealth;
        slider.minValue = 0;
        slider.value = currentHealth;
        
        num.text = currentHealth.ToString() + "/" + maxHealth.ToString();
    }

    public void UpdateHealthbar(int change)
    {
        currentHealth = Mathf.Clamp(currentHealth + change, 0, maxHealth);
        slider.value = currentHealth;
        num.text = currentHealth.ToString() + "/" + maxHealth.ToString();
    }
}
