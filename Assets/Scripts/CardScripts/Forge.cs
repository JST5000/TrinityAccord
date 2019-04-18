using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : CardData
{
    public Forge()
    {
        cardData = new UICardData("Forge", cost: 2, "Deal 4 damage, add a greatsword to discard", UICardData.CardType.ATTACK);
        cost = 2;
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(4);
        addCardToDiscard(new GreatSword());

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
