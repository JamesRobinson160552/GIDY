using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : ScriptableObject
{
    public string itemName;
    public string description;
    public int value;
    public int weight;
    public Sprite itemSprite;
    public int slotNumber;
    [SerializeField] public int vigourMod;
    [SerializeField] public int strengthMod;
    [SerializeField] public int dexterityMod;
    [SerializeField] public int intelligenceMod;
    [SerializeField] public int luckMod;
    public int[] ToArray()
    {
        int[] arr = {vigourMod, strengthMod, dexterityMod, intelligenceMod, luckMod};
        return arr;
    }
}
