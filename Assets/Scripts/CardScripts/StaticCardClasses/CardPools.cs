using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardPools
{

    private static List<CardData> allCards;

    public static List<CardData> GetAllCards()
    {
        if(allCards == null)
        {
            InitAllCards();
        }
        return CardDataUtil.CreateFreshCopiesOf(allCards);
    }

    private static void InitAllCards()
    {
        List<CardData> all = new List<CardData>();
        all.Add(new Backpack());
        all.Add(new Barrage());
        all.Add(new Cannon());
        all.Add(new Claws());
        all.Add(new Clone());
        all.Add(new Dagger());
        all.Add(new Duplicate());
        all.Add(new Energize());
        all.Add(new Flail());
        all.Add(new Hammer());
        all.Add(new Juggle());
        all.Add(new Knockout());
        all.Add(new Lightning());
        all.Add(new Mug());
        all.Add(new Pebble());
        all.Add(new Peer());
        all.Add(new Quickdraw());
        all.Add(new Rally());
        all.Add(new Shock());
        all.Add(new Thought());
        all.Add(new Tide());
        all.Add(new Tome());
        all.Add(new Trick());
        all.Add(new Wand());
        allCards = all;
    }

}
