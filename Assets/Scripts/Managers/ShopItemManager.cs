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

    private bool IsDisabled = false;

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

    public void Start()
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
            GetComponentInParent<ShopManager>().UpdateUI();
        }
    }

    private bool CanPurchase()
    {
        return PermanentState.money >= Data.cost && !SoldOut && Data.OtherRequirementsMet();
    }

    public ShopItem GetItem()
    {
        return Data;
    }

    public void UpdateUI()
    {
        if (Data == null)
        {
            CanvasGroupManip.Disable(ItemImg.GetComponent<CanvasGroup>());
        }
        else
        {
            CanvasGroupManip.Enable(ItemImg.GetComponent<CanvasGroup>());
            if(CanPurchase())
            {
                Enable();
            } else
            {
                Disable();
            }

            if (Data.picture != null)
            {
                ItemImg.color = new Color(1, 1, 1, 1);
                ItemImg.sprite = Data.picture;
            } else
            {
                ItemImg.color = new Color(1, 1, 1, 0);
            }

            if (SoldOut)
            {
                Cost.text = "SOLD OUT!";
            }
            else
            {
                Cost.text = "Cost: " + Data.cost;
            }
            Quantity.text = GetQuantityText();
        }
    }

    public void Disable()
    {
        if (!IsDisabled)
        {
            Button button = GetComponent<Button>();
            button.interactable = false;
            IsDisabled = true;
        }
    }

    public void Enable()
    {
        if (IsDisabled)
        {
            Button button = GetComponent<Button>();
            button.interactable = true;
            IsDisabled = false;
        }
    }

    private string GetQuantityText()
    {
        if(SoldOut)
        {
            return "";
        } else if(Data.limited)
        {
            return "LIMITED!";
        } else
        {
            return "";
        }
    }

}
