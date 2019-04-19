using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSword : CardData
{
    public GreatSword()
    {
        cardData = new UICardData("Great Sword", cost: 2, "Deal 6 damage", UICardData.CardType.ATTACK);
        cost = 2;
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(6);
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
