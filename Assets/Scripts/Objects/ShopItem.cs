using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using TMPro;

public class ShopItem : MonoBehaviour
{
    //TODO: Get random piece of equipment to display
    public BaseItem item;
    [SerializeField] private Image displayImage;
    [SerializeField] private TextMeshProUGUI displayPrice;
    [SerializeField] private ItemDisplay display;
    [SerializeField] private Button buyButton;
    [SerializeField] private PlayerInfo player;
    [SerializeField] private InventoryManager inventory;

    void Start()
    {
        if (!player) { player = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>(); }
        if (!display) { display = GameObject.Find("ItemDisplay").GetComponent<ItemDisplay>(); }
        displayImage.sprite = item.itemSprite;
        displayPrice.text = "$" + item.value.ToString();
    }

    public void PreviewBuy()
    {
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

        player.SetGold(player.gold - item.value);
        inventory.AddItem(item);
        display.gameObject.SetActive(false);
        buyButton.enabled = false;
    }
}
