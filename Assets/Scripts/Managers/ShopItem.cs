using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopItem 
{
    string name;
    int cost;
    string imageName;
    Sprite picture;
    
    public ShopItem(string name, int cost, string imageName)
    {
        this.name = name;
        this.cost = cost;
        LoadPicture(imageName);
    }

    public static GameObject CreateShopItemUI(Transform parent)
    {
        return GameObject.Instantiate(Resources.Load<GameObject>("Choose3Menu"), parent, false);
    }

    //Ex. Health will add health to permanentState or the card shop will open a pack
    public abstract void Effect();

    protected void LoadPicture(string givenSpriteName)
    {
        string folderName = "Shop_Icons/";
        this.picture = Resources.Load<Sprite>(folderName + givenSpriteName);
    }


}
