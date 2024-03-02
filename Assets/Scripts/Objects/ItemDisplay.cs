using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private Image image;

    public void ShowItem(BaseItem item)
    {
        nameText.text = item.itemName;
        priceText.text = "$" + item.value.ToString();
        descriptionText.text = item.description;
        if ((BaseEquipment)item) { statsText.text = GetStats((BaseEquipment)item); }
        image.sprite = item.itemSprite;
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
}
