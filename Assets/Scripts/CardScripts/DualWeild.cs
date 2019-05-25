using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualWeild : CardData
{
    public DualWeild()
    {
        cardData = new UICardData("DualWeild", cost: 2, "Deal 4 damage to target, random card in hand costs 1 until end of encounter", UICardData.CardType.ATTACK);
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(4);
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
        hand[validCards[randomIndex]].GetCardData().setCost(1);
        //Ensure they are playable
        GameObject.Find("Hand").GetComponent<HandManager>().UpdateAllCardsInHand();
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
