using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquippableItem: MonoBehaviour
{
    public EquipmentManager equipmentManager;
    public BaseItem item;
    public int index;
    [SerializeField] private Image displayImage;

    void Start()
    {
        displayImage.sprite = item.itemSprite;
        equipmentManager = GameObject.Find("EquipmentManager").GetComponent<EquipmentManager>();
    }

    public void Preview()
    {
        equipmentManager.Preview(this);
    }
}
