using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quickdraw : CardData
{
    public Quickdraw()
    {
        target = Target.NONE;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Quickdraw", cost: 1, "Discard hand, draw 3", UICardData.CardType.SPELL);
    }

    public override int GetBonusDamage()
    {
        return 0;
    }

    public override void Action(EnemyManager[] enemys)
    {
        DeckManager.Get().DiscardHand();
        draw();
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
