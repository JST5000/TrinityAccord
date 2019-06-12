using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : CardData
{
    public Power()
    {
        target = Target.BOARD;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Power", cost: 0, "Gain 2 energy", UICardData.CardType.SPELL);
    }

    public override void Action(EnemyManager[] enemys)
    {
        addEnergy(2);
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
