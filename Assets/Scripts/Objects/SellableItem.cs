using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SellableItem : MonoBehaviour
{
    public SalesManager salesManager;
    public BaseItem item;
    public int index;
    [SerializeField] private TextMeshProUGUI sellPriceText;
    [SerializeField] private Image displayImage;

    void Start()
    {
        displayImage.sprite = item.itemSprite;
        salesManager = GameObject.Find("SalesManager").GetComponent<SalesManager>();
        sellPriceText.text = (0.7*item.value).ToString();
    }

    public void Preview()
    {
        salesManager.Preview(this);
    }
}
