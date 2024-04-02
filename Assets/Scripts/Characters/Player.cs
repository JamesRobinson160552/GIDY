using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int level;
    [SerializeField] public BaseCharacter baseStats;
    [SerializeField] public BaseItem helmet;
    [SerializeField] public BaseItem armour;
    [SerializeField] public BaseItem lightWeapon;
    [SerializeField] public BaseItem heavyWeapon;
    [SerializeField] public BaseItem magicWeapon;
    public int currentHealth;
    public int[] totalStats;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Awake()
    {
        if (!playerInfo) { playerInfo = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>(); }
        GetEquipment();
        InitializeStats();
    }

    private void GetEquipment()
    {
        helmet = playerInfo.equipped[0];
        armour = playerInfo.equipped[1];
        lightWeapon = playerInfo.equipped[2];
        heavyWeapon = playerInfo.equipped[3];
        magicWeapon = playerInfo.equipped[4];
    }

    private void InitializeStats()
    {
        float levelMultiplier = playerInfo.playerLevel * 0.4f;

        totalStats = new int[] {
            (int)(levelMultiplier * baseStats.vigour), 
            (int)(levelMultiplier * baseStats.strength), 
            (int)(levelMultiplier * baseStats.dexterity), 
            (int)(levelMultiplier * baseStats.intelligence), 
            (int)(levelMultiplier * baseStats.luck)
        };

        if (helmet) {totalStats = ApplyEquimentBonus(totalStats, helmet);}
        if (armour) {totalStats = ApplyEquimentBonus(totalStats, armour);}
        if (lightWeapon) {totalStats = ApplyEquimentBonus(totalStats, lightWeapon);}
        if (heavyWeapon) {totalStats = ApplyEquimentBonus(totalStats, heavyWeapon);}
        if (magicWeapon) {totalStats = ApplyEquimentBonus(totalStats, magicWeapon);}

        currentHealth = totalStats[0];
        healthBar.InitializeHealthBar(baseStats.characterName, totalStats[0]);
    }

    private int[] ApplyEquimentBonus(int[] stats, BaseItem equipment)
    {
        int[] buffs = equipment.ToArray();
        for (int i=0; i<buffs.Length; i++)
        {
            Debug.Log(buffs[i]);
            stats[i] += buffs[i];
        }
        return stats;
    }

    public IEnumerator PlayAttackAnimation(int attackType)
    {
        while (transform.position.x < 1.0f)
        {
            transform.position += new Vector3(0.2f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        switch (attackType)
        {
            case 2: 
                Debug.Log("Player Light Attack Animation");
                BattleSounds.i.PlaySound(0);
                break;
            case 3:
                Debug.Log("Player Heavy Attack Animation");
                BattleSounds.i.PlaySound(1);
                break;
            case 4:
                Debug.Log("Player Magic Attack Animation");
                BattleSounds.i.PlaySound(2);
                break;
            default:
                Debug.Log("Unknown Attack Type");
                break;
        }
        yield return new WaitForSeconds(0.5f);
        while (transform.position.x > -8.0f)
        {
            transform.position -= new Vector3(0.25f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        battleManager.EndPlayerTurn();
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(FlashSprite());
        Debug.Log("Player took " + damage.ToString() + " damage!");
        currentHealth -= damage;
        Debug.Log("Player now has " + currentHealth.ToString() + " health");
        healthBar.UpdateHealthbar(-damage);
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public IEnumerator FlashSprite()
    {
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
    }

    public IEnumerator Die()
    {
        BattleSounds.i.PlaySound(4);
        Debug.Log("Player Died");
        spriteRenderer.color = new Color(1, 1, 1, 1);
        while (spriteRenderer.color.a > 0)
        {
            spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a - 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        battleManager.EndBattle("player");
    }
}
