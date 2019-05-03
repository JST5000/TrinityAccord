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

    private List<ShopItemManager> ShopItems;

    // Start is called before the first frame update
    void Awake()
    {
        List<ShopItem> items = new List<ShopItem>();
        items.Add(new HealthItem(1));
        items.Add(new HealthItem(3));
        items.Add(new HealthItem(7));
        Init(Resources.Load<Sprite>("People/ShopkeeperJames"), "DEFAULT Shop!", items);
        UpdateUI();
    }

    public void Init(Sprite ShopKeeper, string Name, List<ShopItem> items)
    {
        this.ShopKeeper.sprite = ShopKeeper;
        this.Title.text = Name;
        ShopItems = new List<ShopItemManager>();
        foreach(ShopItem item in items)
        {
            
            ShopItems.Add(ShopItem.CreateShopItemUI(ShopItemParent, item).GetComponent<ShopItemManager>());
        }
        UpdateUI();
    }

    public void Exit()
    {
        Destroy(gameObject);
    }

    private void UpdateUI()
    {
        if (ShopItems == null)
        {
            CanvasGroupManip.Disable(GetComponent<CanvasGroup>());
        }
        else
        {
            CanvasGroupManip.Enable(GetComponent<CanvasGroup>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
