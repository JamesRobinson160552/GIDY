using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SalesManager : MonoBehaviour
{
    [SerializeField] private GameObject preview;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private Image image;

    private SellableItem currentItem;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private InventoryManager inventory;
    [SerializeField] private Vector3 startOfScroll;
    [SerializeField] private GameObject sellableItemPrefab;
    [SerializeField] private float itemBuffer;
    [SerializeField] private GameObject sellablesContainer;
    public int numSellables;

    void Start()
    {
        RefreshSellables();
    }

    public void Preview(SellableItem itemToDisplay)
    {
        UiSounds.instance.PlaySound(0);
        preview.SetActive(true);
        currentItem = itemToDisplay;
        nameText.text = itemToDisplay.item.itemName;
        priceText.text = "$" + (0.7*itemToDisplay.item.value).ToString();
        descriptionText.text = itemToDisplay.item.description;
        if ((BaseEquipment)itemToDisplay.item) { statsText.text = GetStats((BaseEquipment)itemToDisplay.item); }
        image.sprite = itemToDisplay.item.itemSprite;
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

    public void Sell()
    {
        UiSounds.instance.PlaySound(2);
        inventory.RemoveItem(currentItem.item);
        RemoveSellable(currentItem);
        playerInfo.SetGold(playerInfo.gold + (int)(0.7*currentItem.item.value));
        preview.SetActive(false);
    }

    public void AddSellable(BaseItem item)
    {
        Vector3 newItemPosition = new Vector3(0,0,0);
        numSellables = sellablesContainer.transform.childCount;
        if (numSellables == 0) { newItemPosition = startOfScroll; }
        else
        {
            Transform lastItemTransform = null;
            foreach (Transform t in sellablesContainer.transform)
            {
                lastItemTransform = t;
            }
            newItemPosition = new Vector3(lastItemTransform.localPosition.x + itemBuffer, lastItemTransform.localPosition.y, 0);
        }
        GameObject newItem = Instantiate(sellableItemPrefab, newItemPosition, Quaternion.identity);
        newItem.transform.SetParent(sellablesContainer.transform);
        newItem.transform.localScale = new Vector3(1f, 1f, 1f);
        newItem.transform.localPosition = newItemPosition;
        newItem.GetComponent<SellableItem>().item = item;
        newItem.GetComponent<SellableItem>().index = numSellables;
        numSellables++;
    }

    public void RemoveSellable(SellableItem item)
    {
        Destroy(item.gameObject);
        int removedItemIndex = item.index;
        numSellables--;
        foreach (Transform t in sellablesContainer.transform)
        {
            SellableItem i = t.gameObject.GetComponent<SellableItem>();
            if (i.index > removedItemIndex)
            {
                i.index--;
                t.localPosition = new Vector3(
                    t.localPosition.x - itemBuffer,
                    t.localPosition.y,
                    t.localPosition.z
                );
            }
        }
    }

    public void RefreshSellables()
    {
        foreach (BaseItem i in inventory.inventory)
        {
            AddSellable(i);
        }
    }
}

