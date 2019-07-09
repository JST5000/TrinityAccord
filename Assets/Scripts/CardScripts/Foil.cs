using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foil : CardData
{
    private int expectedCost = 2;
    public Foil()
    {
        target = Target.ENEMY;
    }


    protected override UICardData CreateUICardData()
    {
        return new UICardData("Foil", cost: GetCost(), "Deal " + GetDamage() + " damage, reduce cost by 1", UICardData.CardType.ATTACK);
    }

    private int GetDamage()
    {
        return 3 + sharpened;
    }

    private int GetCost()
    {
        return (int)Mathf.Max(0, expectedCost);
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(GetDamage());
        expectedCost -= 1;
        UpdateUICardData(doNotUpdateCost: false);
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
