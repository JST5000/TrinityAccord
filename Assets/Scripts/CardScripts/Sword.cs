using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : CardData
{
    public Sword()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Sword", cost: 2, "Deal " + GetDamage() + " damage.", UICardData.CardType.ATTACK, "Sword");
    }

    private int GetDamage()
    {
        return 3 + GetBonusDamage();
    }


    public override void Action(EnemyManager[] enemys)
    {
        SoundManager.playSound("Sword1");
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

