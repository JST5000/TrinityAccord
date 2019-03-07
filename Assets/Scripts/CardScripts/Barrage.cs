using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrage : CardData
{
    public Barrage()
    {
        cardData = new UICardData("Barrage", cost: 1, "Deal 1 damage for each other attack played this turn", UICardData.CardType.ATTACK);
        cost = 0;
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(getNumberOfAttacksPlayed()-1);
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
