using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unearth : CardData
{
    public Unearth()
    {
        cardData = new UICardData("Unearth", cost: 0, "Draw 1 from discard", UICardData.CardType.SPELL);
        cost = 0;
        target = Target.BOARD;
    }

    public override void Action(EnemyManager[] enemys)
    {

        drawFromDiscard();
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
