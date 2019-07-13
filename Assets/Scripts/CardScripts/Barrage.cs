using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrage : CardData
{
    public Barrage()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        string effect = "Deal 1 damage for each other attack played this turn";
        if(sharpenDamage > 0)
        {
            effect += " (+" + sharpenDamage + ")";
        }
        return new UICardData("Barrage", cost: 0, effect, UICardData.CardType.ATTACK);
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(getNumberOfAttacksPlayed()-1+sharpenDamage);
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
