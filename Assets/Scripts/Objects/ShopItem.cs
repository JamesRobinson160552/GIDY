using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public BaseItem item;
    [SerializeField] private Image displayImage;
    [SerializeField] private TextMeshProUGUI displayPrice;
    [SerializeField] private ItemDisplay display;
    [SerializeField] private Button buyButton;
    [SerializeField] private PlayerInfo player;
    [SerializeField] private InventoryManager inventory;
    [SerializeField] private int index;
    public bool bought;

    public void Init()
    {
        if (!player) { player = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>(); }
        if (!display) { display = GameObject.Find("ItemDisplay").GetComponent<ItemDisplay>(); }
        if (!inventory) { inventory = GameObject.Find("InventoryManager").GetComponent<InventoryManager>(); }
        if (bought)
        {
            buyButton.enabled = false;
            display.gameObject.SetActive(false);
        }
        else
        {
            displayImage.sprite = item.itemSprite;
            displayPrice.text = "$" + item.value.ToString();
            buyButton.enabled = true;
        }
    }

    public void PreviewBuy()
    {
        UiSounds.instance.PlaySound(0);
        display.gameObject.SetActive(true);
        display.ShowItem(this);
    }

    public void Buy()
    {
        if (player.gold < item.value)
        {
            Debug.Log("Not Enough Gold!");
            return;
        }

        UiSounds.instance.PlaySound(2);
        player.SetGold(player.gold - item.value);
        inventory.AddItem(item);
        display.gameObject.SetActive(false);
        buyButton.enabled = false;
        bought = true;
        PlayerPrefs.SetInt("bought" + index, 1);
    }
}
