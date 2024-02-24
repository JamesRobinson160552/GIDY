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
    [SerializeField] private BattleManager battleManager;

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

    public IEnumerator PlayAttackAnimation(int attackType)
    {
        switch (attackType)
        {
            case 2: 
                Debug.Log("Player Light Attack Animation");
                break;
            case 3:
                Debug.Log("Player Heavy Attack Animation");
                break;
            case 4:
                Debug.Log("Player Magic Attack Animation");
                break;
            default:
                Debug.Log("Unknown Attack Type");
                break;
        }
        yield return new WaitForSeconds(1);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Player took " + damage.ToString() + " damage!");
        currentHealth -= damage;
        Debug.Log("Player now has " + currentHealth.ToString() + " health");
        healthBar.UpdateHealthbar(-damage);
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die()
    {
        Debug.Log("Player Died");
        //Play Death Animation
        yield return new WaitForSeconds(1);
        battleManager.EndBattle("player");
    }
}
