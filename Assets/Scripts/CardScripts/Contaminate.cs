using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contaminate : CardData
{
    public Contaminate()
    {
        cardData = new UICardData("Contaminate", cost: 1, "Turns random card in hand into contagion card", UICardData.CardType.SPELL);
        target = Target.BOARD;
    }

    public override void Action(EnemyManager[] enemys)
    {
        int mode = UnityEngine.Random.Range(1, 4);
        CardData tempCardData=null;
        switch (mode)
        {
            case 1:
                tempCardData = new Power();
                break;
            case 2:
                tempCardData = new VileSword();
                break;
            case 3:
                tempCardData = new Greed();
                break;

        }
        List<int> validCards = new List<int>();
        CardManager[] hand = getHand();
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
        hand[validCards[randomIndex]].Init(tempCardData);
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

