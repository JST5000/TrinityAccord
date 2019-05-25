using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greed : CardData
{
    public Greed()
    {
        cardData = new UICardData("Greed", cost: 0, "Draw 2", UICardData.CardType.SPELL);
        target = Target.BOARD;
    }

    public override void Action(EnemyManager[] enemys)
    {
        draw();
        draw();
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
