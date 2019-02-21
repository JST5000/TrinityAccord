using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : CardData
{
    public Sword()
    {
        cardData = new UICardData("Sword", cost: 2, "Deal 3 damage.", UICardData.CardType.ATTACK);
        cost = 2;
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(3);
    }
    public override void Action(CardData[] cards)
    {

    }
    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {

    }


}

