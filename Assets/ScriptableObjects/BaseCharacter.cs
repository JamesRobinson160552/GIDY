using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Create new Character")]
public class BaseCharacter : ScriptableObject
{
    [SerializeField] public string characterName;
    [SerializeField] public Sprite sprite;
    [SerializeField] public int vigour;
    [SerializeField] public int strength;
    [SerializeField] public int dexterity;
    [SerializeField] public int intelligence;
    [SerializeField] public int luck;
}

//TODO: add condition effects (eg burn) and/or damage types
