using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] private BaseEnemy[] enemies;
    private Enemy currentEnemy;
    [SerializeField] private Vector3 spawnPosition;
    public static EnemySpawner i;

    private void Start()
    {
        if (i == null) { i = this; }
        SpawnNew();
    }

    public void SpawnNew()
    {
        BaseEnemy stats = enemies[Random.Range(0, enemies.Length)];
        currentEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity).GetComponent<Enemy>();
        currentEnemy.baseStats = stats;
        currentEnemy.GetComponent<SpriteRenderer>().sprite = stats.sprite;
        currentEnemy.Init();
        BattleManager.i.enemy = currentEnemy;
        BattleSounds.i.PlaySound(5);
    }
}
