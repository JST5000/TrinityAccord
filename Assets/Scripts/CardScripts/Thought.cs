using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thought : CardData
{
    public Thought()
    {
        fragile = true;
        target = Target.BOARD;
    }

    protected override UICardData CreateUICardData()
    {
       return new UICardData("Thought", cost: 1, "Flip Idea", UICardData.CardType.SPELL);
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
