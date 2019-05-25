using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSword : CardData
{
    public GreatSword()
    {
        cardData = new UICardData("Great Sword", cost: 2, "Deal 6 damage", UICardData.CardType.ATTACK);
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(6+sharpened);
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
        cardData = new UICardData("Great Sword", cost: 2, "Deal " + (6 + sharpened) + " damage.", UICardData.CardType.ATTACK);
    }
}
