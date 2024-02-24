using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int level;
    [SerializeField] public BaseCharacter baseStats;
    [SerializeField] public BaseArmour helmet;
    [SerializeField] public BaseArmour armour;
    [SerializeField] public BaseWeapon lightWeapon;
    [SerializeField] public BaseWeapon heavyWeapon;
    [SerializeField] public BaseWeapon magicWeapon;
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
        
        if (helmet) {totalStats = ApplyEquimentBonus(totalStats, helmet);}
        if (armour) {totalStats = ApplyEquimentBonus(totalStats, armour);}
        if (lightWeapon) {totalStats = ApplyEquimentBonus(totalStats, lightWeapon);}
        if (heavyWeapon) {totalStats = ApplyEquimentBonus(totalStats, heavyWeapon);}
        if (magicWeapon) {totalStats = ApplyEquimentBonus(totalStats, magicWeapon);}

        currentHealth = totalStats[0];
        healthBar.InitializeHealthBar(baseStats.characterName, totalStats[0]);
    }

    private int[] ApplyEquimentBonus(int[] stats, BaseEquipment equipment)
    {
        int[] buffs = equipment.ToArray();
        for (int i=0; i<stats.Length; i++)
        {
            stats[i] += buffs[i];
        }
        return stats;
    }
}
