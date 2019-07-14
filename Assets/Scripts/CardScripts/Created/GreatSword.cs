using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSword : CardData
{
    public GreatSword()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Great Sword", cost: 2, "Deal " + GetDamage() + " damage", UICardData.CardType.ATTACK);
    }

    private int GetDamage()
    {
        return 6 + GetBonusDamage();
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
