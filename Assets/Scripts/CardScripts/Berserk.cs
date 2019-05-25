using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserk : CardData
{
    public Berserk()
    {
        cardData = new UICardData("Berserk", cost: 2, "Play top 2 cards of deck at random", UICardData.CardType.SPELL, "Berserk");
        target = Target.BOARD;
    }

    public override void Action(EnemyManager[] enemys)
    {
        for (int i = 0; i < 2; i++)
        {
            CardData top = grabTop();
            if (top == null)
            {
                return;
            }
            playCardRandomTarget(top);
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

