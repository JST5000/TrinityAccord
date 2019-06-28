using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff2 : CardData
{
    public Staff2()
    {
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("B", cost: 1, "Deal " + GetDamage() + " damage Draw 1 card", UICardData.CardType.ATTACK);
    }

    private int GetDamage()
    {
        return 1 + sharpened;
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
