using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class PlayerInfo : MonoBehaviour, ISavable
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
    [SerializeField] private ItemSlot[] itemSlots;
    bool refreshOnAwake = true;

    public void Awake()
    {
        if (refreshOnAwake)
        {
            SavingSystem.i.Load("equipment");
            refreshOnAwake = false;
        }
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
            UiSounds.instance.PlaySound(5);
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
        PlayerPrefs.SetInt("exp", exp);
    }

    public void SetLevelThreshold()
    {
        nextLevelThreshold = Mathf.Pow((playerLevel/expRequirementModifier), expRequirementGrowthRate);
        levelText.text = playerLevel.ToString();
    }

    public void SetGold(int newGold)
    {
        Debug.Log("Setting gold to " + newGold.ToString());
        gold = newGold;
        goldText.text = "$" + gold.ToString();
        PlayerPrefs.SetInt("gold", gold);
    }

    public object CaptureState()
    {
        //Debug.Log("Capturing state of equipment");
        Dictionary<string, object> state = new Dictionary<string, object>();
        for (int i = 0; i < equipped.Length; i++)
        {
            if (equipped[i] != null)
            {
                state[i.ToString()] = equipped[i].itemName;
            }
            else
            {
                state[i.ToString()] = null;
            }
        }
        return state;
    }

    public void RestoreState(object state)
    {
        //Debug.Log("Restoring state of equipment");
        Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
        for (int i = 0; i < equipped.Length; i++)
        {
            if (itemSlots[0] == null) itemSlots[0] = GameObject.Find("EquipmentManager").GetComponent<EquipmentManager>().slots[0];
            if (itemSlots[1] == null) itemSlots[1] = GameObject.Find("EquipmentManager").GetComponent<EquipmentManager>().slots[1];
            if (itemSlots[2] == null) itemSlots[2] = GameObject.Find("EquipmentManager").GetComponent<EquipmentManager>().slots[2];
            if (itemSlots[3] == null) itemSlots[3] = GameObject.Find("EquipmentManager").GetComponent<EquipmentManager>().slots[3];
            if (itemSlots[4] == null) itemSlots[4] = GameObject.Find("EquipmentManager").GetComponent<EquipmentManager>().slots[4];
            if (stateDict[i.ToString()] != null)
            {
                equipped[i] = inventory.FindItem(stateDict[i.ToString()].ToString());
                itemSlots[i].currentlyEquipped = equipped[i];
            }
            itemSlots[i].Init();
        }
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
