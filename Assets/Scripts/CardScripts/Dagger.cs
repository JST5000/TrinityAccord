using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : CardData
{
    public Dagger()
    {
        cardData = new UICardData("Dagger", cost: 1, "Deal 1 damage.", UICardData.CardType.ATTACK);
        cost = 1;
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(1);
    }
    public override void Action(CardData[] cards)
    {

    }
    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {

    }


}
