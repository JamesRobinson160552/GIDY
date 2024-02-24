using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level = 1;
    [SerializeField] public BaseEnemy baseStats;
    public int currentHealth;
    public int[] totalStats;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] public int preferredAttackType;
    [SerializeField] private BattleManager battleManager;
    public bool isDead = false;

    void Awake()
    {
        InitializeStats();
        //Preferred attack type will default to the highest stat, but can be overidden in the editor
        if (preferredAttackType == 0)
        {
            SetPreferredAttackType();
        }
    }

    private void InitializeStats()
    {
        totalStats = new int[] {
            baseStats.vigour * level, 
            baseStats.strength * level, 
            baseStats.dexterity * level, 
            baseStats.intelligence * level, 
            baseStats.luck * level
            };
        currentHealth = totalStats[0];
        healthBar.InitializeHealthBar(baseStats.characterName, totalStats[0]);
    }

    private void SetPreferredAttackType()
    {
        if (totalStats[2] > totalStats[3] && totalStats[2] > totalStats[4]) { preferredAttackType = 2; }
        else if (totalStats[2] > totalStats[3] && totalStats[2] > totalStats[4]) { preferredAttackType = 2; }
        else if (totalStats[2] > totalStats[3] && totalStats[2] > totalStats[4]) { preferredAttackType = 2; }
        else { preferredAttackType = Random.Range(2,4); }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy took " + damage.ToString() + " damage!");
        currentHealth -= damage;
        Debug.Log("Enemy now has " + currentHealth.ToString() + " health");
        healthBar.UpdateHealthbar(-damage);
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die()
    {
        isDead = true;
        Debug.Log("Enemy Died");
        //Play Death Animation
        yield return new WaitForSeconds(1);
        DropLoot();
        battleManager.EndBattle("enemy");
    }

    public IEnumerator PlayAttackAnimation(int attackType)
    {
        switch (attackType)
        {
            case 2: 
                Debug.Log("Enemy Light Attack Animation");
                break;
            case 3:
                Debug.Log("Enemy Heavy Attack Animation");
                break;
            case 4:
                Debug.Log("Enemy Magic Attack Animation");
                break;
            default:
                Debug.Log("Unknown Enemy Attack Type");
                break;
        }
        yield return new WaitForSeconds(1);
    }

    public void DropLoot()
    {
        Debug.Log("Dropping Loot");
    }
}
