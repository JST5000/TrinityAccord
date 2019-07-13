using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : CardData
{
    public Shuriken()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Shuriken", cost: 1, "Deal " + GetDamage() + " damage", UICardData.CardType.ATTACK);
    }

    private int GetDamage()
    {
        return 3 + sharpenDamage;
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
