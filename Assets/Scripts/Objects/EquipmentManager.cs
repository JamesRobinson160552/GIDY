using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private GameObject preview;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private Image image;

    private EquippableItem currentItem;
    [SerializeField] private InventoryManager inventory;
    [SerializeField] private Vector3 startOfScroll;
    [SerializeField] private GameObject equippableItemPrefab;
    [SerializeField] private float itemBuffer;
    [SerializeField] private GameObject equippablesContainer;
    public int numEquippables;
    [SerializeField] public ItemSlot[] slots = new ItemSlot[5];

    void Start()
    {
        //AssetDatabase.Refresh();
        StartCoroutine(RefreshEquippables());
    }

    void onEnable()
    {
        StartCoroutine(RefreshEquippables());
    }

    public void Preview(EquippableItem itemToDisplay)
    {
        UiSounds.instance.PlaySound(0);
        preview.SetActive(true);
        currentItem = itemToDisplay;
        nameText.text = itemToDisplay.item.itemName;
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

    public void Equip()
    {
        slots[currentItem.item.slotNumber].Equip(currentItem.item);
        RemoveEquippable(currentItem);
        preview.SetActive(false);
    }

    public void AddEquippable(BaseItem item)
    {
        Vector3 newItemPosition = new Vector3(0,0,0);
        numEquippables = equippablesContainer.transform.childCount;
        if (numEquippables == 0) { newItemPosition = startOfScroll; }
        else
        {
            Transform lastItemTransform = null;
            foreach (Transform t in equippablesContainer.transform)
            {
                lastItemTransform = t;
            }
            newItemPosition = new Vector3(lastItemTransform.localPosition.x + itemBuffer, lastItemTransform.localPosition.y, 0);
        }
        GameObject newItem = Instantiate(equippableItemPrefab, newItemPosition, Quaternion.identity);
        newItem.transform.SetParent(equippablesContainer.transform);
        newItem.transform.localScale = new Vector3(1f, 1f, 1f);
        newItem.transform.localPosition = newItemPosition;
        newItem.GetComponent<EquippableItem>().item = item;
        newItem.GetComponent<EquippableItem>().index = numEquippables;
        numEquippables++;
    }

    public void RemoveEquippable(EquippableItem item)
    {
        Destroy(item.gameObject);
        int removedItemIndex = item.index;
        numEquippables--;
        foreach (Transform t in equippablesContainer.transform)
        {
            EquippableItem i = t.gameObject.GetComponent<EquippableItem>();
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

    public void daBuffy()
    {
        StartCoroutine(RefreshEquippables());
    }

    public IEnumerator RefreshEquippables()
    {
        foreach (Transform t in equippablesContainer.transform)
        {
            Destroy(t.gameObject);
        }

        numEquippables = 0;
        yield return new WaitForEndOfFrame(); //Make sure all objects are registered as destroyed before readding

        foreach (BaseItem i in inventory.inventory)
        {
            AddEquippable(i);
        }
    }
}

