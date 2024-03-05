using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Create new Consumable")]
public class BaseConsumable : BaseItem
{
    [SerializeField] public int healthRestore;
    [SerializeField] public int duration;

    public int[] ToArray()
    {
        int[] arr = {vigourMod, strengthMod, dexterityMod, intelligenceMod, luckMod, healthRestore, duration};
        return arr;
    }
}
