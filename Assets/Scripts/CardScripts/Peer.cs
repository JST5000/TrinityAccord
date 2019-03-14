using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peer : CardData
{
    public Peer()
    {
        cardData = new UICardData("Peer", cost: 0, "Look at top 2 cards of deck and pick one", UICardData.CardType.SPELL);
        cost = 0;
        target = Target.BOARD;
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
