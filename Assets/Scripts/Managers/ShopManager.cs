using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    //Has formatting component to automatically format the generated shop items
    public Transform ShopItemParent;
    public TextMeshProUGUI Title;
    public Image ShopKeeper;
    public TextMeshProUGUI FeedbackDisplay;

    private bool showHealth = false;
    private List<ShopItemManager> ShopItems;

    public void Init(Sprite ShopKeeper, string Name, List<ShopItem> Items)
    {
        this.ShopKeeper.sprite = ShopKeeper;
        this.Title.text = Name;
        ShopItems = new List<ShopItemManager>();
        foreach(ShopItem item in Items)
        {
            
            ShopItems.Add(ShopItem.CreateShopItemUI(ShopItemParent, item).GetComponent<ShopItemManager>());
        }
        UpdateUI();
    }

    public void Init(Sprite ShopKeeper, string Name, List<ShopItem> Items, bool ShowHealth)
    {
        this.showHealth = ShowHealth;
        Init(ShopKeeper, Name, Items);
    }

    public void Exit()
    {
        Destroy(gameObject);
    }

    public void UpdateUI()
    {
        if (ShopItems == null)
        {
            CanvasGroupManip.Disable(GetComponent<CanvasGroup>());
        }
        else
        {
            CanvasGroupManip.Enable(GetComponent<CanvasGroup>());
        }
        ShopItemManager[] items = GetComponentsInChildren<ShopItemManager>();
        foreach(ShopItemManager item in items)
        {
            item.UpdateUI();
        }
        UpdateFeedbackDisplay();
    }

    private void UpdateFeedbackDisplay()
    {
        if (ShopItems.Count == 0)
        {
            FeedbackDisplay.text = "Out of stock for now, sorry!";
        } else
        {
            FeedbackDisplay.text = "Welcome!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
