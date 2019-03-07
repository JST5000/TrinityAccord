using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : CardData
{
    public Backpack()
    {
        cardData = new UICardData("Backpack", cost: 0, "Draw 2 Discard 2", UICardData.CardType.SPELL);
        cost = 0;
        target = Target.BOARD;
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
        card.discard();
        if (checkNumberOfCardsInHand() == 0)
        {
            return 10;
        }
        return 1;
    }
}
