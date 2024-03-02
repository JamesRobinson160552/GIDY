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
    [SerializeField] private Button buyButton;
    private ShopItem currentShopItem;

    public void ShowItem(ShopItem shopItem)
    {
        currentShopItem = shopItem;
        nameText.text = shopItem.item.itemName;
        priceText.text = "$" + shopItem.item.value.ToString();
        descriptionText.text = shopItem.item.description;
        if ((BaseEquipment)shopItem.item) { statsText.text = GetStats((BaseEquipment)shopItem.item); }
        image.sprite = shopItem.item.itemSprite;
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

    public void Buy()
    {
        currentShopItem.Buy();
    }
}
