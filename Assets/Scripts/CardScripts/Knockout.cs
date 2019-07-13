using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockout : CardData
{
    public Knockout()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Knockout", cost: 2, "Deal " + GetDamage() + " damage Stun", UICardData.CardType.ATTACK);
    }

    private int GetDamage()
    {
        return 3 + sharpenDamage;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(GetDamage());
        enemys[0].Stun();
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