using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Create new Consumable")]
public class BaseConsumable : BaseItem
{
    [SerializeField] public int healthRestore;
    [SerializeField] public int duration;
    [SerializeField] public int vigourMod;
    [SerializeField] public int strengthMod;
    [SerializeField] public int dexterityMod;
    [SerializeField] public int intelligenceMod;
    [SerializeField] public int luckMod;
}
