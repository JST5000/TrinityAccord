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

    public static GameObject CreateShopItemUI(Transform parent, ShopItem item)
    {
        GameObject instance = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/ShopItem"), parent, false);
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
