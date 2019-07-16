using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightmare : CardData
{
    public Nightmare()
    {
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(GetDamage());
        enemys[0].WakeUp(); //Explicit call to avoid race conditions
        enemys[0].Drowsy();
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

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Nightmare", cost: 3, "Deal " + GetDamage() + " damage and put an enemy to sleep next turn.", UICardData.CardType.ATTACK, "Knightmare");
    }

    private int GetDamage()
    {
        return 3 + GetBonusDamage();
    }
}
