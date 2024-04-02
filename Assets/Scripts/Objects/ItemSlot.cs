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
    [SerializeField] private EquipmentManager equipmentManager;

    public void Init()
    {
        if (currentlyEquipped) 
        {
            sprite.sprite = currentlyEquipped.itemSprite;
            sprite.color = new Color(1, 1, 1, 1);
        }

        else 
        {
            sprite.color = new Color(0, 0, 0, 0);
        }
    }

    public void Equip(BaseItem item)
    {
        UiSounds.instance.PlaySound(3);
        if (currentlyEquipped)
        {
            Unequip();
        }
        inventory.RemoveItem(item);
        currentlyEquipped = item;
        playerInfo.equipped[slotNumber] = item;
        SavingSystem.i.Save("equipped");
        sprite.sprite = currentlyEquipped.itemSprite;
        sprite.color = new Color(1, 1, 1, 1);
    }

    public void Unequip()
    {
        inventory.AddItem(currentlyEquipped);
        UiSounds.instance.PlaySound(1);
        StartCoroutine(equipmentManager.RefreshEquippables());
        currentlyEquipped = null;
        playerInfo.equipped[slotNumber] = null;
        SavingSystem.i.Save("equipped");
        sprite.sprite = null;
        sprite.color = new Color(0, 0, 0, 0);
    }

    public void Preview()
    {
        UiSounds.instance.PlaySound(0);
        if (currentlyEquipped)
        {
            display.gameObject.SetActive(true);
            display.ShowItem(this);
        }
    }
}
