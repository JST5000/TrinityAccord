using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopItem 
{
    public string name;
    public int cost;
    public string imageName;
    public Sprite picture;
    public bool limited;

    public CardData card;

    public ShopItem(string name, int cost, string imageName)
    {
        this.name = name;
        this.cost = cost;
        LoadPicture(imageName);
        this.limited = false;
    }

    public ShopItem(string name, int cost, string imageName, bool limited)
    {
        this.name = name;
        this.cost = cost;
        LoadPicture(imageName);
        this.limited = limited;
    }

    /// <summary>
    /// Used to have a card shop item using the existing prefab
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cost"></param>
    /// <param name="limited"></param>
    public ShopItem(string name, int cost, CardData card, bool limited)
    {
        this.card = card;
        this.name = name;
        this.cost = cost;
        this.limited = limited;
    }

    public static GameObject CreateShopItemUI(Transform parent, ShopItem item)
    {
        string prefabPath = "Prefabs/ShopItem";
        if (item.card != null)
        {
            prefabPath = "Prefabs/CardItem";
        }

        GameObject instance = GameObject.Instantiate(Resources.Load<GameObject>(prefabPath), parent, false);

        if(item.card != null)
        {
            CardManager cardMan = instance.GetComponentInChildren<CardManager>();
            cardMan.Init(item.card);
            cardMan.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        instance.GetComponent<ShopItemManager>().Init(item);

        return instance;
    }

    //Ex. Health will add health to permanentState or the card shop will open a pack
    public abstract void Effect();

    public virtual bool OtherRequirementsMet()
    {
        return true;
    }

    protected void LoadPicture(string givenSpriteName)
    {
        string folderName = "Shop_Icons/";
        this.picture = Resources.Load<Sprite>(folderName + givenSpriteName);
    }
}
