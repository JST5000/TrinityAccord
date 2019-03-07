using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockout : CardData
{
    public Knockout()
    {
        cardData = new UICardData("Knockout", cost: 2, "Deal 3 damage Stun", UICardData.CardType.ATTACK);
        cost = 2;
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(3);
        enemys[0].Stun();
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