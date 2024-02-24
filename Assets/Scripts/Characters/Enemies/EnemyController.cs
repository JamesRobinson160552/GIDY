using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private BaseEnemy stats;
    private int currentHealth;
    public Enemy(BaseEnemy stats)
    {
        this.stats = stats;
    }

    void Start()
    {
        currentHealth = stats.vigour;
    }
}
