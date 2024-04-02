using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;
using UnityEditor;

public class InventoryManager : MonoBehaviour, ISavable
{
    public BaseItem[] armour = new BaseItem[100];
    public BaseItem[] heavyWeapons = new BaseItem[100];
    public BaseItem[] helmets = new BaseItem[100];
    public BaseItem[] lightWeapons = new BaseItem[100];
    public BaseItem[] magicWeapons = new BaseItem[100];
    public BaseItem[] consumables = new BaseItem[100];

    public List<BaseItem> inventory = new List<BaseItem>();
    List<BaseItem> playerEquippables = new List<BaseItem>();
    List<BaseItem> playerConsumables = new List<BaseItem>();
    public List<string> inventoryList = new List<string>();
    [SerializeField] private SalesManager salesManager;

    void Awake()
    {
        LoadInventory();
    }

    public void LoadInventory()
    {
        SavingSystem.i.Load("inventory");

        for (int i=0; i<inventoryList.Count; i++)
        {
            string currentItem = inventoryList[i];
            Debug.Log(currentItem);
            if (currentItem.Length > 1)
            {
                inventory.Add(FindItem(currentItem));
            }
        }
    }

    public BaseItem CheckArray(BaseItem[] itemType, string nameOfItem)
    {
        for (int i=0; i<itemType.Length; i++)
        {
            if (itemType[i] != null)
            {
                if (itemType[i].itemName == nameOfItem)
                {
                    return (itemType[i]);
                }
            }
        }
        return null;
    }

    public BaseItem FindItem(string name)
    {
        BaseItem target = null;
        target = CheckArray(armour, name);
        if (target == null) target = CheckArray(heavyWeapons, name);
        if (target == null)target = CheckArray(helmets, name);
        if (target == null)target = CheckArray(lightWeapons, name);
        if (target == null)target = CheckArray(magicWeapons, name);
        if (target == null)target = CheckArray(consumables, name);
        return target;
    }

    public void AddItem(BaseItem item)
    {
        inventory.Add(item);
        inventoryList.Add(item.itemName);
        salesManager.AddSellable(item);
        SavingSystem.i.Save("inventory");
    }

    public void RemoveItem(BaseItem item)
    {
        inventory.Remove(item);
        inventoryList.Remove(item.itemName);
        SavingSystem.i.Save("inventory");
    }

    public object CaptureState()
    {
        Debug.Log("Capturing state of inventory");
        Dictionary<string, object> state = new Dictionary<string, object>();
        for (int i = 0; i < inventoryList.Count; i++)
        {
            state[i.ToString()] = inventoryList[i];
        }
        return state;
    }

    public void RestoreState(object state)
    {
        Debug.Log("Restoring state of inventory");
        inventoryList.Clear();
        Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
        for (int i = 0; i < stateDict.Count; i++)
        {
            inventoryList.Add(stateDict[i.ToString()].ToString());
        }
    }
}
