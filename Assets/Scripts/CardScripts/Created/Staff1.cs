using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff1 : CardData
{
    public Staff1()
    {
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("A", cost: 0, "Deal " + GetDamage() + " damage", UICardData.CardType.ATTACK);
    }

    private int GetDamage()
    {
        return 2 + sharpened;
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
