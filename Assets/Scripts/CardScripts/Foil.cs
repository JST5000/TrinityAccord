using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foil : CardData
{
    private int growEnergy = 0;
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
        return (int)Mathf.Max(0, 2 - growEnergy);
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(GetDamage());
        growEnergy += 1;
        UpdateUICardData();
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
