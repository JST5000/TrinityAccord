using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff3 : CardData
{
    public Staff3()
    {
        cardData = new UICardData("C", cost: 0, "Draw 2 cards", UICardData.CardType.ATTACK);
    }

    public override void Action(EnemyManager[] enemys)
    {
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

