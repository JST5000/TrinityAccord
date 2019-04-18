using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multishot : CardData
{
    private int growDamage = 0;
    public Multishot()
    {
        cardData = new UICardData("Multishot", cost: 2, "Deal 3 damage Charge 3", UICardData.CardType.ATTACK);
        cost = 2;
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(3 + growDamage);
        growDamage = 0;
        cardData = new UICardData("Multishot", cost: 2, "Deal 3 damage Charge 3", UICardData.CardType.ATTACK);

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
        growDamage += 3;
        cardData = new UICardData("Multishot", cost: 2, "Deal "+(3+growDamage)+ " damage Charge 3", UICardData.CardType.ATTACK);

    }
}
