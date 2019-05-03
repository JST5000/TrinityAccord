using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemManager : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI Quantity;
    public Image ItemImg;

    private ShopItem Data;
    private bool SoldOut;

    public void Init(ShopItem data)
    {
        Title.text = data.name;
        this.Data = data;
        SoldOut = false;
        UpdateUI();
    }

    public void Awake()
    {
        UpdateUI();
    }

    public void Purchase()
    {
        if (CanPurchase()) {
            if (Data.limited)
            {
                SoldOut = true;
            }
            PermanentState.money -= Data.cost;
            Data.Effect();
        }
    }

    private bool CanPurchase()
    {
        return PermanentState.money >= Data.cost && !SoldOut;
    }

    public ShopItem GetItem()
    {
        return Data;
    }

    private void UpdateUI()
    {
        if (Data == null)
        {
            CanvasGroupManip.Disable(ItemImg.GetComponent<CanvasGroup>());
        }
        else
        {
            CanvasGroupManip.Enable(ItemImg.GetComponent<CanvasGroup>());

            ItemImg.sprite = Data.picture;
            Cost.text = "Cost: " + Data.cost;
            Quantity.text = GetQuantityText();
        }
    }

    private string GetQuantityText()
    {
        if(SoldOut)
        {
            return "SOLD OUT!";
        } else if(Data.limited)
        {
            return "LIMITED!";
        } else
        {
            return "";
        }
    }

}
