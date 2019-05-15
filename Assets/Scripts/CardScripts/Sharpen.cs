﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharpen : CardData
{
    public Sharpen()
    {
        cardData = new UICardData("Sharpen", cost: 1, "Increase damage of cards in hand", UICardData.CardType.SPELL);
        cost = 1;
        target = Target.BOARD;
    }

    public override void Action(EnemyManager[] enemys)
    {
        List<int> validCards = new List<int>();
        CardManager[] hand = getHand();
        for (int i = 0; i < hand.Length; ++i)
        {
            if (!hand[i].IsEmpty())
            {
                validCards.Add(i);
            }
        }
        for (int i = 0; i < validCards.Count; i++)
        {
            hand[validCards[i]].GetCardData().sharpen();
        }
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