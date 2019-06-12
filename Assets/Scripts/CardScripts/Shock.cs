using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock : CardData
{
    public Shock()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Shock", cost: 2, "Stagger target", UICardData.CardType.SPELL);
    }


    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Stagger();
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

