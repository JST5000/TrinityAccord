using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff3 : CardData
{
    public Staff3()
    {
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("C", cost: 0, "Draw 2 cards", UICardData.CardType.ATTACK);
    }

    public override void Action(EnemyManager[] enemys)
    {
        throw new System.NotImplementedException();

    }
    public override void Action(CardData[] cards)
    {
        throw new System.NotImplementedException();


    }
    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {
        throw new System.NotImplementedException();

    }

    public override int SecondAction(CardManager card)
    {
        throw new System.NotImplementedException();
    }
}

