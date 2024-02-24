using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
public int level;
    [SerializeField] public BaseEnemy baseStats;
    public int currentHealth;
    public int[] totalStats;
    [SerializeField] private HealthBar healthBar;

    void Awake()
    {
        InitializeStats();
    }

    private void InitializeStats()
    {
        totalStats = new int[] {baseStats.vigour, baseStats.strength, baseStats.dexterity, baseStats.intelligence, baseStats.luck};
        currentHealth = totalStats[0];
        healthBar.InitializeHealthBar(baseStats.characterName, totalStats[0]);
    }
}
