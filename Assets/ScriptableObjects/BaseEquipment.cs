using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEquipment : BaseItem
{
    [SerializeField] public EquipmentSlot slot;

    public int[] ToArray() 
    {
        int[] arr = {vigourMod, strengthMod, dexterityMod, intelligenceMod, luckMod};
        return arr;
    }
}

public enum EquipmentSlot
{
    helmet,
    armour, 
    lightWeapon,
    heavyWeapon,
    magicWeapon
}
