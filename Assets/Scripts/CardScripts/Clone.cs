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
        return new UICardData("Clone", cost: 0, "Becomes copy of target until end of encounter", UICardData.CardType.SPELL);
    }

    public override int GetBonusDamage()
    {
        return 0;
    }

    public override void Action(EnemyManager[] enemys)
    {

    }
    public override void Action(CardData[] cards)
    {
        CardData copy = this;
        if (cards.Length != 0)
        {
            copy = cards[0].CloneCard();
            copy.SetCost(cards[0].GetCost());
        }

        CardManager cardInHand = getMyCardManager();
        if (cardInHand != null)
        {
            cardInHand.Init(copy);
        }
        else
        {
            DeckManager dm = DeckManager.Get();
            if (!dm.addCardToHand(copy))
            {
                DeckManager.Get().AddToDiscard(copy);
            }
        }
    }
    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {

    }

    public override int SecondAction(CardManager card)
    {
        throw new System.NotImplementedException();
    }
}
