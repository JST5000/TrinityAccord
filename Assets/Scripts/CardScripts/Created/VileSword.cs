using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VileSword : CardData
{
    public VileSword()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("VileSword", cost: 2, "Deal " + GetDamage() + " damage", UICardData.CardType.ATTACK, "Vile_Sword");
    }

    private int GetDamage()
    {
        return 5 + GetBonusDamage();
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
