using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mug : CardData
{
    public Mug()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Mug", cost: 2, "Deal " + GetDamage() + " damage. If the enemy is stunned or killed gain 2 energy, draw a card", UICardData.CardType.ATTACK);
    }

    private int GetDamage()
    {
        return 2 + sharpened;
    }


    public override void Action(EnemyManager[] enemys)
    {

        enemys[0].Damage(GetDamage());
        if (enemys[0].IsEmpty() || enemys[0].IsStunned())
        {
            addEnergy(2);
            draw();
        }
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
