using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;
using UnityEditor;

public class InventoryManager : MonoBehaviour
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
        AssetDatabase.Refresh();
        inventoryList = new List<string>();
        //Get tasks from file
        StreamReader reader = new StreamReader(GetPath());
        while (reader.Peek() >= 0)
        {
            inventoryList.Add(reader.ReadLine());
        }
        reader.Close(); 

        foreach (string s in inventoryList)
        {
            CheckArray(armour, s);
            CheckArray(heavyWeapons, s);
            CheckArray(helmets, s);
            CheckArray(lightWeapons, s);
            CheckArray(magicWeapons, s);
            CheckArray(consumables, s);
        }
    }

    void CheckArray(BaseItem[] itemType, string nameOfItem)
    {
        for (int i=0; i<itemType.Length; i++)
        {
            if (itemType[i] != null)
            {
                if (itemType[i].itemName == nameOfItem)
                {
                    inventory.Add(itemType[i]);
                }
            }
        }
    }

    public void SaveInventory()
    {
        using (var stream = new FileStream(GetPath(), FileMode.Truncate))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                foreach (BaseItem i in inventory)
                {
                    writer.WriteLine(i.itemName);
                }
                writer.Close();
            }
            stream.Close();
        }
    }

    private string GetPath()
    {
        #if UNITY_EDITOR
        return Application.dataPath+"/Data/"+"inventory.txt";
        #elif UNITY_ANDROID
        return Application.persistentDataPath+"inventory.txt";
        #else
        return Application.persistentDataPath+"/inventory.txt";
        #endif
    }

    public void AddItem(BaseItem item)
    {
        inventory.Add(item);
        //salesManager.AddSellable(item);
        SaveInventory();
    }

    public void RemoveItem(BaseItem item)
    {
        inventory.Remove(item);
        SaveInventory();
    }
}
