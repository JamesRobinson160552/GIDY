using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public BaseCharacter stats;
    [SerializeField] public BaseArmour helmet;
    [SerializeField] public BaseArmour armour;
    [SerializeField] public BaseWeapon lightWeapon;
    [SerializeField] public BaseWeapon heavyWeapon;
    [SerializeField] public BaseWeapon magicWeapon;
    private int currentHealth;

    void Start()
    {
        currentHealth = stats.vigour;
    }
}
