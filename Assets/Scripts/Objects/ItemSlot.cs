using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public BaseEquipment currentlyEquipped;
    [SerializeField] private InventoryManager inventory;
    [SerializeField] private int slotNumber;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private EquippedDisplay display;

    private void Start()
    {
        currentlyEquipped = (BaseEquipment)playerInfo.equipped[slotNumber];
    }

    public void Equip(BaseEquipment item)
    {
        if (currentlyEquipped)
        {
            Unequip();
        }
        inventory.RemoveItem(item);
        currentlyEquipped = item;
        playerInfo.SaveEquipped();
    }

    public void Unequip()
    {
        inventory.AddItem(currentlyEquipped);
        currentlyEquipped = null;
        playerInfo.SaveEquipped();
    }

    public void Preview()
    {
        display.gameObject.SetActive(true);
        display.ShowItem(this);
    }
}
