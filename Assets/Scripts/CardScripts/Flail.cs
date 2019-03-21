using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flail : CardData
{
    public Flail()
    {
        cardData = new UICardData("Flail", cost: 1, "Deal 3 damage to random enemy", UICardData.CardType.ATTACK);
        cost = 1;
        target = Target.BOARD;
    }

    public override void Action(EnemyManager[] enemies)
    {
        damageRandom(3);
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
