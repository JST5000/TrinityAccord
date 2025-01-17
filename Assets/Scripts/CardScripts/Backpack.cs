﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : CardData
{
    public Backpack()
    {
        target = Target.NONE;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Backpack", cost: 0, "Draw 2 Discard 2", UICardData.CardType.SPELL, "Backpack");
    }

    public override int GetBonusDamage()
    {
        return 0;
    }

    public override void Action(EnemyManager[] enemys)
    {
        draw();
        draw();
        selectCard(2);
    }
    public override void Action(CardData[] cards)
    {

      
    }
    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {

    }

    public override int SecondAction(CardManager card)
    {
        DiscardCardOnStack(card.GetCardData());
        card.SetEmpty();
        if (checkNumberOfCardsInHand() == 0)
        {
            return 10;//Exits card select mode if there arent enough cards in hand
        }
        return 1;//Standard decrement is one if there are still cards to select
    }
}
