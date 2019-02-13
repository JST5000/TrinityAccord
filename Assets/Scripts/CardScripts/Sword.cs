using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : CardData
{
    public Sword()
    {
        cardData = new UICardData("Sword", cost: 2, "Deal 3 damage.", UICardData.CardType.ATTACK);
    }

    public override string CardName()
    {
        return "Sword";
    }

    public override int Cost()
    {
        return 2;
    }

    public override Target Target()
    {
        return global::Target.ENEMY;//(int)global::Target.Enemies;
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

