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

    private void GetItem()
    {
        BaseItem[] itemArray;
        switch (itemType)
        {
            case "armour":
                itemArray = inventory.armour;
                break;
            case "helmet":
                itemArray = inventory.helmets;
                break;
            case "light":
                itemArray = inventory.lightWeapons;
                break;
            case "heavy":
                itemArray = inventory.heavyWeapons;
                break;
            case "magic":
                itemArray = inventory.magicWeapons;
                break;
            case "consumables":
                itemArray = inventory.consumables;
                break;
            default:
                itemType = types[Random.Range(0, types.Length-1)];
                GetItem();
                return;
        }

        //While loop since this might miss if the array is not full
        while (item == null)
        {
            int randomIndex = Random.Range(0, itemArray.Length-1);
            item = itemArray[randomIndex];
        }
    }
}
