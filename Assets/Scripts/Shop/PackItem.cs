using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackItem : ShopItem
{

    public PackItem() : base("Mixed Pack", GetCost(), imageName: "CardBack")
    {
    }

    private static int GetCost()
    {
        return 3;
    }

    override public void  Effect()
    {
        //Open a pack
        GameObject instance = GameObject.Instantiate(Resources.Load<GameObject>("Choose3Menu"), GameObject.Find("Canvas").transform, false);
        Choose3Manager choose3 = instance.GetComponent<Choose3Manager>();
        List<CardData> options = CardDataUtil.ChooseNWithoutReplacement(CardPools.GetAllDraftableCards(), 3);
        choose3.DoNotLoadAnotherScene();
        choose3.Init(options.ToArray());
    }

}
