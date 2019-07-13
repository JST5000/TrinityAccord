using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claws : CardData
{
    public Claws()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Claws", cost: 2, "Deal " + GetPrimaryDamage() + " damage to target and " + GetSecondaryDamage() + " damage to random enemy", UICardData.CardType.ATTACK, "Claws");
    }

    private int GetPrimaryDamage()
    {
        return 3 + sharpenDamage;
    }

    private int GetSecondaryDamage()
    {
        return 2 + sharpenDamage;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(GetPrimaryDamage());
        damageRandom(GetSecondaryDamage());
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
