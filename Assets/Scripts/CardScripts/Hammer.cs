using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : CardData
{
    public Hammer()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Hammer", cost: 2, "Deal " + GetLowerBound() + "-" + GetUpperBound() + " damage at random", UICardData.CardType.ATTACK);
    }

    private int GetLowerBound()
    {
        return 1 + GetBonusDamage();
    }

    private int GetUpperBound()
    {
        return 7 + GetBonusDamage();
    }

    private int GetDamage()
    {
        return Random.Range(GetLowerBound(), GetUpperBound());
    }


    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(GetDamage());
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
