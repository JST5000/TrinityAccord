using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : CardData
{
    private int growDamage = 0;
    public Storm()
    {
        cardData = new UICardData("Storm", cost: 3, "Deal 2 damage to all enemies Charge 1", UICardData.CardType.ATTACK);
        cost = 3;
        target = Target.BOARD;
    }

    public override void Action(EnemyManager[] enemys)
    {
        foreach (EnemyManager enemy in enemys)
        {
            enemy.Damage(2 + growDamage);

        }
        growDamage = 0;
        cardData = new UICardData("Storm", cost: 3, "Deal 2 damage to all enemies Charge 1", UICardData.CardType.ATTACK);

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
    public override void onDiscard()
    {
        growDamage += 1;
        cardData = new UICardData("Storm", cost: 3, "Deal " + (2 + growDamage) + " damage to all enemies Charge 1", UICardData.CardType.ATTACK);

    }
}
