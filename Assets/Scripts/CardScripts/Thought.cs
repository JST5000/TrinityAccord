using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thought : CardData
{
    public Thought()
    {
        cardData = new UICardData("Thought", cost: 1, "Flip Idea", UICardData.CardType.SPELL);
        cost = 1;
        fragile = true;
        target = Target.BOARD;
    }

    public override void Action(EnemyManager[] enemys)
    {
        addCardToDiscard(new Idea());
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
