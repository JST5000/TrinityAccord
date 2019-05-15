using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mug : CardData
{
    public Mug()
    {
        cardData = new UICardData("Mug", cost: 2, "Deal 2 damage. If the enemy is stunned or killed gain 2 energy, draw a card", UICardData.CardType.ATTACK);
        cost = 2;
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {

        enemys[0].Damage(2+sharpened);
        if (enemys[0].IsEmpty() || enemys[0].IsStunned())
        {
            addEnergy(2);
            draw();
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
        cardData = new UICardData("Mug", cost: 2, "Deal " + (3 + sharpened) + " damage. If the enemy is stunned or killed gain 2 energy, draw a card", UICardData.CardType.ATTACK);
    }
}
