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
    public PlayerInfo playerInfo;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Player player;
    [SerializeField] private float levelMultiplier = 0.4f;

    public void Init()
    {
        if (!battleManager) { battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>(); }
        if (!healthBar) { healthBar = GameObject.Find("EnemyHUD").GetComponent<HealthBar>(); }
        if (!playerInfo) { playerInfo = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>(); }
        if (!player) { player = GameObject.Find("Player").GetComponent<Player>(); }
        InitializeStats();
        //Preferred attack type will default to the highest stat, but can be overidden in the editor
        if (preferredAttackType == 0)
        {
            SetPreferredAttackType();
        }
    }

    private void InitializeStats()
    {
        level = playerInfo.playerLevel + Random.Range(-2, 2);
        totalStats = new int[] {
            (int)(baseStats.vigour * level * levelMultiplier), 
            (int)(baseStats.strength * level * levelMultiplier), 
            (int)(baseStats.dexterity * level * levelMultiplier), 
            (int)(baseStats.intelligence * level * levelMultiplier), 
            (int)(baseStats.luck * level * levelMultiplier)
            };
        currentHealth = totalStats[0];
        healthBar.InitializeHealthBar(baseStats.characterName, totalStats[0]);
        StartCoroutine("FadeIn");
    }

    private void SetPreferredAttackType()
    {
        if (totalStats[2] > totalStats[3] && totalStats[2] > totalStats[4]) { preferredAttackType = 2; }
        else if (totalStats[3] > totalStats[2] && totalStats[3] > totalStats[4]) { preferredAttackType = 3; }
        else if (totalStats[4] > totalStats[2] && totalStats[4] > totalStats[3]) { preferredAttackType = 4; }
        else { preferredAttackType = Random.Range(2,4); }
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(FlashSprite());
        Debug.Log("Enemy took " + damage.ToString() + " damage!");
        currentHealth -= damage;
        Debug.Log("Enemy now has " + currentHealth.ToString() + " health");
        healthBar.UpdateHealthbar(-damage);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        BattleSounds.i.PlaySound(3);
        isDead = true;
        Debug.Log("Enemy Died");
        //Play Death Animation
        DropLoot();
    }

    public IEnumerator PlayAttackAnimation(int attackType)
    {
        while (transform.position.x > 1.0f)
        {
            transform.position -= new Vector3(0.2f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        switch (attackType)
        {
            case 2: 
                Debug.Log("Enemy Light Attack Animation");
                BattleSounds.i.PlaySound(0);
                break;
            case 3:
                Debug.Log("Enemy Heavy Attack Animation");
                BattleSounds.i.PlaySound(1);
                break;
            case 4:
                Debug.Log("Enemy Magic Attack Animation");
                BattleSounds.i.PlaySound(2);
                break;
            default:
                Debug.Log("Unknown Enemy Attack Type");
                break;
        }
        yield return new WaitForSeconds(0.5f);
        while (transform.position.x < 8.0f)
        {
            transform.position += new Vector3(0.25f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        battleManager.StartPlayerTurn();
    }

    public IEnumerator FadeIn()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0);
        while (spriteRenderer.color.a < 1)
        {
            spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a + 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        battleManager.StartBattle();
    }

    public IEnumerator FadeOut()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        while (spriteRenderer.color.a > 0)
        {
            spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a - 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        EnemySpawner.i.SpawnNew();
        Destroy(gameObject);
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

    public void DropLoot()
    {
        Debug.Log("Dropping Loot");
        playerInfo.SetGold(playerInfo.gold + (int)(baseStats.goldDrop * level/2 * Random.Range(0.9f, player.totalStats[4]/10 + 1)));
        StartCoroutine(FadeOut());
    }
}
