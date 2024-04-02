using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour, ISavable
{
    [SerializeField] ShopItem[] items;
    public bool[] bought;
    [SerializeField] BaseItem[] stock;
    [SerializeField] private InventoryManager allItems;

    void Start()
    {
        for (int i = 0; i < bought.Length; i++)
        {
            bought[i] = PlayerPrefs.GetInt("bought" + i, 0) == 1 ? true : false;
        }

        if (Timer.i.newDay)
        {
            for (int i = 0; i < bought.Length; i++)
            {
                bought[i] = false;
                PlayerPrefs.SetInt("bought" + i.ToString(), 0);
            }
            stock[0] = GetItem(1);
            stock[1] = GetItem(0);
            stock[2] = GetItem(1);
            stock[3] = GetItem(2);
            stock[4] = GetItem(3);
            stock[5] = GetItem(4);
            stock[6] = GetItem(2);
            stock[7] = GetItem(3);
            stock[8] = GetItem(4);

            SavingSystem.i.Save("stock");
        }
        
        else
        {
            SavingSystem.i.Load("stock");
        }

        for (int i = 0; i < items.Length; i++)
        {
            items[i].item = stock[i];
            items[i].bought = bought[i];
            items[i].Init();
        }
    }

    private BaseItem GetItem(int slot)
    {
        BaseItem[] itemArray = new BaseItem[100];
        switch (slot)
        {
            case 0:
                itemArray = allItems.helmets;
                break;
            case 1:
                itemArray = allItems.armour;
                break;
            case 2:
                itemArray = allItems.lightWeapons;
                break;
            case 3:
                itemArray = allItems.heavyWeapons;
                break;
            case 4:
                itemArray = allItems.magicWeapons;
                break;
            case 5:
                itemArray = allItems.consumables;
                break;
            default:
                itemArray = allItems.helmets;
                break;
        }

        int count = 0;
        BaseItem item = null;
        //While loop since this might miss if the array is not full
        while (item == null && count < 10)
        {
            int randomIndex = Random.Range(0, itemArray.Length-1);
            item = itemArray[randomIndex];
            count ++;
        }
        return item;
    }

    public object CaptureState()
    {
        Debug.Log("Capturing state of store");
        Dictionary<string, object> state = new Dictionary<string, object>();
        for (int i = 0; i < stock.Length; i++)
        {
            state[i.ToString()] = stock[i].itemName;
        }
        return state;
    }

    public void RestoreState(object state)
    {
        Debug.Log("Restoring state of store");
        Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
        for (int i = 0; i < stock.Length; i++)
        {
            stock[i] = allItems.FindItem(stateDict[i.ToString()].ToString());
        }
    }
}
