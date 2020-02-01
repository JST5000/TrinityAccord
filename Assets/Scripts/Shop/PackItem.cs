using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackItem : ShopItem
{

    public PackItem(int cost = 3) : base("Mixed Pack", cost, imageName: "CardBack")
    {
    }

    override public void  UniqueEffect()
    {
        //Open a pack
        GameObject instance = GameObject.Instantiate(Resources.Load<GameObject>("Choose3Menu"), GameObject.Find("Canvas").transform, false);
        Choose3Manager choose3 = instance.GetComponent<Choose3Manager>();
        List<CardData> options = CardDataUtil.ChooseNWithoutReplacement(CardPools.GetAllDraftableCards(), 3);
        choose3.DoNotLoadAnotherScene();
        choose3.Init(options.ToArray());
    }

}
