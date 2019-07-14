using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : CardData
{
    public Kunai()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Kunai", cost: 2, "Deal " + GetDamage() + " damage, add a shuriken to discard", UICardData.CardType.ATTACK);
    }

    private int GetDamage()
    {
        return 3 + GetBonusDamage();
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(GetDamage());
        addCardToDiscard(new Shuriken());

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
