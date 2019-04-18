using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTonHammer : CardData
{
    public OneTonHammer()
    {
        cardData = new UICardData("One-Ton-Hammer", cost: 3, "Deal 9 damage to target, random card in hand costs 3 until end of encounter", UICardData.CardType.ATTACK);
        cost = 3;
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(9);
        CardManager[] hand = getHand();
        List<int> validCards = new List<int>();
        for (int i = 0; i < hand.Length; ++i)
        {
            if (!hand[i].IsEmpty())
            {
                validCards.Add(i);
            }
        }
        //Nothing to damage
        if (validCards.Count == 0)
        {
            return;
        }
        int randomIndex = UnityEngine.Random.Range(0, validCards.Count);
        hand[validCards[randomIndex]].GetCardData().setChangedCost(3);
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
