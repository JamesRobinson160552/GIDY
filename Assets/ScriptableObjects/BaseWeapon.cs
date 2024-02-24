using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Create new Weapon")]
public class BaseWeapon : BaseEquipment
{
    [SerializeField] public int accuracy;
}
