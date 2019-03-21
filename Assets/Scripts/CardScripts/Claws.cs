using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claws : CardData
{
    public Claws()
    {
        cardData = new UICardData("Claws", cost: 2, "Deal 3 damage to target and 2 damage to random enemy", UICardData.CardType.ATTACK);
        cost = 2;
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(3);
        damageRandom(2);
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
