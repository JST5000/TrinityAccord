using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : CardData
{
    public Shuriken()
    {
        cardData = new UICardData("Shuriken", cost: 1, "Deal 3 damage", UICardData.CardType.ATTACK);
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(3+sharpened);
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
        cardData = new UICardData("Shuriken", cost: 1, "Deal " + (3 + sharpened) + " damage.", UICardData.CardType.ATTACK);
    }
}
