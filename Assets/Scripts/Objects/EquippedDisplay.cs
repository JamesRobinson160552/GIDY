using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquippedDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private Image image;
    private ItemSlot currentItem;

    public void ShowItem(ItemSlot itemToDisplay)
    {
        currentItem = itemToDisplay;
        nameText.text = currentItem.currentlyEquipped.itemName;
        descriptionText.text = currentItem.currentlyEquipped.description;
        if ((BaseEquipment)itemToDisplay.currentlyEquipped) { statsText.text = GetStats((BaseEquipment)itemToDisplay.currentlyEquipped); }
        image.sprite = currentItem.currentlyEquipped.itemSprite;
    }

    private string GetStats(BaseEquipment item)
    {
        return (
            "V: " + item.vigourMod.ToString() +
            " | S: " + item.strengthMod.ToString() +
            " | D: " + item.dexterityMod.ToString() +
            " | I: " + item.intelligenceMod.ToString() +
            " | L: " + item.luckMod.ToString()
        );
    }

    public void Unequip()
    {
        UiSounds.instance.PlaySound(1);
        currentItem.Unequip();
    }
}
