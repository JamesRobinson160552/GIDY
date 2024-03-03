using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private float expRequirementModifier;
    [SerializeField] private float expRequirementGrowthRate;
    public int playerLevel;
    public int exp;
    public float nextLevelThreshold;
    public int gold;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Slider expBar;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private InventoryManager inventory;
    public BaseItem[] equipped = new BaseItem[5];

    public void Awake()
    {
        playerLevel = PlayerPrefs.GetInt("playerLevel", 1);
        exp = PlayerPrefs.GetInt("exp", 0);
        gold = PlayerPrefs.GetInt("gold", 0);
        goldText.text = "$" + gold.ToString();
        SetLevelThreshold();
        SetExpBar();
    }

    private void SetExpBar()
    {
        expBar.minValue = 0;
        expBar.maxValue = nextLevelThreshold;
        expBar.value = exp;
    }

    public void GainExp(int expToGain)
    {
        exp += expToGain;
        if (exp >= nextLevelThreshold)
        {
            playerLevel += 1;
            PlayerPrefs.SetInt("playerLevel", playerLevel);
            exp = Mathf.FloorToInt((float)exp - nextLevelThreshold);
            SetLevelThreshold();
            SetExpBar();
        }
        else
        {
            expBar.value = exp;
        }
    }

    public void SetLevelThreshold()
    {
        nextLevelThreshold = Mathf.Pow((playerLevel/expRequirementModifier), expRequirementGrowthRate);
        levelText.text = playerLevel.ToString();
    }

    public void SetGold(int newGold)
    {
        gold = newGold;
        goldText.text = "$" + gold.ToString();
        PlayerPrefs.SetInt("gold", gold);
    }

    private string GetPath()
    {
        #if UNITY_EDITOR
        return Application.dataPath+"/Data/"+"equipped.txt";
        #elif UNITY_ANDROID
        return Application.persistentDataPath+"equipped.txt";
        #else
        return Application.persistentDataPath+"/equipped.txt";
        #endif
    }

    public void SaveEquipped()
    {
        using (var stream = new FileStream(GetPath(), FileMode.Truncate))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                foreach (BaseItem i in equipped)
                {
                    writer.WriteLine(i.itemName);
                }
                writer.Close();
            }
            stream.Close();
        }
    }

    public void LoadEquipped()
    {
        AssetDatabase.Refresh();
        List<string> equippedList = new List<string>();
        StreamReader reader = new StreamReader(GetPath());
        
        while (reader.Peek() >= 0)
        {
            equippedList.Add(reader.ReadLine());
        }
        reader.Close();

        equipped[0] = CheckArray(inventory.armour, equippedList[0]);
        equipped[1] = CheckArray(inventory.helmets, equippedList[1]);
        equipped[2] = CheckArray(inventory.lightWeapons, equippedList[2]);
        equipped[3] = CheckArray(inventory.heavyWeapons, equippedList[3]);
        equipped[4] = CheckArray(inventory.magicWeapons, equippedList[4]);
    }

    private BaseItem CheckArray(BaseItem[] itemType, string nameOfItem)
    {
        for (int i=0; i<itemType.Length; i++)
        {
            if (itemType[i] != null)
            {
                if (itemType[i].itemName == nameOfItem)
                {
                    return itemType[i];
                }
            }
        }
        return null;
    }

}
