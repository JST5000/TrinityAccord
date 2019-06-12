﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quickdraw : CardData
{
    public Quickdraw()
    {
        target = Target.BOARD;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Quickdraw", cost: 1, "Discard hand, draw 3", UICardData.CardType.SPELL);
    }


    public override void Action(EnemyManager[] enemys)
    {
        GameObject.Find("Deck").GetComponent<DeckManager>().DiscardHand();
        draw();
        draw();
        draw();
    }
    public override void Action(CardData[] cards)
    {

    }
    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {

    }

    public override int SecondAction(CardManager card)
    {
        throw new System.NotImplementedException();
    }
}
