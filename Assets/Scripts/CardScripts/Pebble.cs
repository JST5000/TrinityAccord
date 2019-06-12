using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pebble : CardData
{
    public Pebble()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
         return new UICardData("Pebble", cost: 0, "Deal + " + GetDamage() + " damage Draw 1 card", UICardData.CardType.ATTACK, "Pebble");
    }

    private int GetDamage()
    {
        return 1 + sharpened;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(GetDamage());
        draw();
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
