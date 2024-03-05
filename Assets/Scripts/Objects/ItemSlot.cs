using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public BaseItem currentlyEquipped;
    [SerializeField] private InventoryManager inventory;
    [SerializeField] private int slotNumber;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private EquippedDisplay display;
    [SerializeField] private Image sprite;

    private void Start()
    {
        currentlyEquipped = (BaseEquipment)playerInfo.equipped[slotNumber];
        sprite.sprite = currentlyEquipped.itemSprite;
    }

    public void Equip(BaseItem item)
    {
        if (currentlyEquipped)
        {
            Unequip();
        }
        inventory.RemoveItem(item);
        currentlyEquipped = item;
        playerInfo.SaveEquipped();
        sprite.sprite = currentlyEquipped.itemSprite;
    }

    public void Unequip()
    {
        inventory.AddItem(currentlyEquipped);
        currentlyEquipped = null;
        playerInfo.equipped[slotNumber] = null;
        playerInfo.SaveEquipped();
        sprite.sprite = null;
    }

    public void Preview()
    {
        if (currentlyEquipped)
        {
            display.gameObject.SetActive(true);
            display.ShowItem(this);
        }
    }
}