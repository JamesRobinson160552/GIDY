using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Create new Enemy")]
public class BaseEnemy : BaseCharacter
{
    [SerializeField] public int goldDrop;
}
