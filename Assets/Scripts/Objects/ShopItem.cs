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
    [SerializeField] private InventoryManager allItems;
    [Header("Can be: armour / helmet / light / heavy / magic / consumable")]
    [SerializeField] private string itemType;
    string[] types = {"armour", "helmets", "light", "heavy", "magic", "consumables"};

    void Start()
    {
        GetItem();
        if (!player) { player = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>(); }
        if (!display) { display = GameObject.Find("ItemDisplay").GetComponent<ItemDisplay>(); }
        displayImage.sprite = item.itemSprite;
        displayPrice.text = "$" + item.value.ToString();
    }

    public void PreviewBuy()
    {
        UiSounds.instance.PlaySound(0);
        display.gameObject.SetActive(true);
        display.ShowItem(this);
    }

    public void Buy()
    {
        UiSounds.instance.PlaySound(2);
        if (player.gold < item.value)
        {
            Debug.Log("Not Enough Gold!");
            return;
        }

        player.SetGold(player.gold - item.value);
        allItems.AddItem(item);
        display.gameObject.SetActive(false);
        buyButton.enabled = false;
    }

    private void GetItem()
    {
        BaseItem[] itemArray;
        switch (itemType)
        {
            case "armour":
                itemArray = allItems.armour;
                break;
            case "helmet":
                itemArray = allItems.helmets;
                break;
            case "light":
                itemArray = allItems.lightWeapons;
                break;
            case "heavy":
                itemArray = allItems.heavyWeapons;
                break;
            case "magic":
                itemArray = allItems.magicWeapons;
                break;
            case "consumables":
                itemArray = allItems.consumables;
                break;
            default:
                itemType = types[Random.Range(0, types.Length-1)];
                GetItem();
                return;
        }

        int count = 0;
        //While loop since this might miss if the array is not full
        while (item == null && count < 10)
        {
            int randomIndex = Random.Range(0, itemArray.Length-1);
            item = itemArray[randomIndex];
            count ++;
        }
    }
}
