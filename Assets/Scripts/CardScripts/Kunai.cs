using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : CardData
{
    public Kunai()
    {
        cardData = new UICardData("Kunai", cost: 2, "Deal 3 damage, add a shuriken to discard", UICardData.CardType.ATTACK);
        cost = 2;
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(3);
        addCardToDiscard(new Shuriken());

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
