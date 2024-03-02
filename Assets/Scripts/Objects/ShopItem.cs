using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using TMPro;

public class ShopItem : MonoBehaviour
{
    //TODO: Get random piece of equipment to display
    public BaseItem item;
    [SerializeField] private Image displayImage;
    [SerializeField] private TextMeshProUGUI displayPrice;
    [SerializeField] private ItemDisplay display;

    void Start()
    {
        if (!display) { display = GameObject.Find("ItemDisplay").GetComponent<ItemDisplay>(); }
        displayImage.sprite = item.itemSprite;
        displayPrice.text = "$" + item.value.ToString();
    }

    public void PreviewBuy()
    {
        display.gameObject.SetActive(true);
        display.ShowItem(item);
    }
}
