using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEquipment : BaseItem
{
    [SerializeField] public EquipmentSlot slot;
    [SerializeField] public Sprite sprite;
    [SerializeField] public int vigourMod;
    [SerializeField] public int strengthMod;
    [SerializeField] public int dexterityMod;
    [SerializeField] public int intelligenceMod;
    [SerializeField] public int luckMod;
}

public enum EquipmentSlot
{
    helmet,
    armour, 
    lightWeapon,
    heavyWeapon,
    magicWeapon
}
