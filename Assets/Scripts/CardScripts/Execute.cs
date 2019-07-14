using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Execute : CardData
{
    public Execute()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Execute", cost: 2, "Deal " + GetInitialDamage() + " damage. If the enemy is stunned, deal " + GetAdditionalDamage() + " more", 
            UICardData.CardType.ATTACK);
    }

    private int GetInitialDamage()
    {
        return 2 + GetBonusDamage();
    }

    private int GetAdditionalDamage()
    {
        return 5 + GetBonusDamage();
    }

    public override void Action(EnemyManager[] enemys)
    {

        enemys[0].Damage(GetInitialDamage());
        if (enemys[0].IsEmpty())
        {
            return;
        }
        if(enemys[0].IsStunned())
        {
            enemys[0].Damage(GetAdditionalDamage());

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
