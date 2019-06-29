using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardPools
{

    private static List<CardData> allDraftableCards;
    private static List<CardData> allCardsIncludingDefaults;


    public static List<CardData> GetAllDraftableCards()
    {
        if(allDraftableCards == null)
        {
            InitAllDraftableCards();
        }
        return CardDataUtil.CreateFreshCopiesOf(allDraftableCards);
    }

    private static void InitAllDraftableCards()
    {
        List<CardData> all = new List<CardData>();
        all.Add(new Backpack());
        all.Add(new Barrage());
        all.Add(new Cannon());
        all.Add(new Claws());
        all.Add(new Clone());
        //Duplicate is currently removed from the game
        //all.Add(new Duplicate());
        all.Add(new Flail());
        all.Add(new Hammer());
        all.Add(new Juggle());
        all.Add(new Knockout());
        all.Add(new Lightning());
        all.Add(new Mug());
        all.Add(new Pebble());
        all.Add(new Quickdraw());
        all.Add(new Rally());
        all.Add(new Shock());
        all.Add(new Thought());
        all.Add(new Tide());
        all.Add(new Tome());
        all.Add(new Trick());
        all.Add(new Wand());
        all.Add(new Peer());
        all.Add(new Bash());
        all.Add(new Berserk());
        all.Add(new Chaos());
        all.Add(new Contaminate());
        all.Add(new Crossbow());
        all.Add(new DualWield());
        all.Add(new Nightmare());
        all.Add(new Yawn());
        all.Add(new Virus());
        all.Add(new Execute());
        all.Add(new Foil());
        all.Add(new Forge());
        all.Add(new Kunai());
        all.Add(new Multishot());
        all.Add(new OneTonHammer());
        all.Add(new Storm());
        all.Add(new Throw());
        all.Add(new Wisdom());
        all.Add(new Unearth());
        all.Add(new Staff());
        all.Add(new Sharpen());
//        all.Add(new TargetCard()); Readd when fixing, or when fixed

        allDraftableCards = all;
    }

    public static List<CardData> GetAllCardsIncludingDefaults()
    {
        if (allCardsIncludingDefaults == null)
        {
            allCardsIncludingDefaults = CardDataUtil.CreateFreshCopiesOf(GetAllDraftableCards());
            allCardsIncludingDefaults.Add(new Energize());
            allCardsIncludingDefaults.Add(new Sword());
            allCardsIncludingDefaults.Add(new Dagger());
        }
        return allCardsIncludingDefaults;
    }

}
