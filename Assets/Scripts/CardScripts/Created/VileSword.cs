using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VileSword : CardData
{
    public VileSword()
    {
        cardData = new UICardData("VileSword", cost: 2, "Deal 5 damage", UICardData.CardType.ATTACK);
        cost = 2;
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(5);
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
