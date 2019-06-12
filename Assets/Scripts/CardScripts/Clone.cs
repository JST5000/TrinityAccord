﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : CardData
{
    public Clone()
    {
        target = Target.CARD;
        fragile = true;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Clone", cost: 0, "Becomes copy of target until end of game", UICardData.CardType.SPELL);
    }


    public override void OnSelectedInHand()
    {
        base.OnSelectedInHand();
        GameObject.Find("Hand").GetComponent<HandManager>().EnableAllCardsInHand();
    }

    public override void Action(EnemyManager[] enemys)
    {

    }
    public override void Action(CardData[] cards)
    {
        getMyCardManager().Init(cards[0].Clone());

    }
    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {

    }

    public override int SecondAction(CardManager card)
    {
        throw new System.NotImplementedException();
    }
}
