using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Execute : CardData
{
    public Execute()
    {
        cardData = new UICardData("Execute", cost: 2, "Deal 2 damage. If the enemy is staggered, deal 5 more", UICardData.CardType.ATTACK);
        cost = 2;
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {

        enemys[0].Damage(2+sharpened);
        if (enemys[0].IsEmpty())
        {
            return;
        }
        if(enemys[0].IsStunned())
        {
            enemys[0].Damage(5);

        }
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
    public override void sharpen()
    {
        sharpened++;
        cardData = new UICardData("Execute", cost: 2, "Deal " + (2 + sharpened) + " If the enemy is staggered, deal 5 more", UICardData.CardType.ATTACK);
    }
}
