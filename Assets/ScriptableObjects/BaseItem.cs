using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : ScriptableObject
{
    [SerializeField] public string description;
    [SerializeField] public int value;
    [SerializeField] public int weight;
}
