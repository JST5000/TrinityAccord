using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : CardData
{
    public Forge()
    {
        cardData = new UICardData("Forge", cost: 3, "Deal 4 damage, add a Great Sword to discard", UICardData.CardType.ATTACK);
        cost = 3;
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(4+sharpened);
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
    public override void sharpen()
    {
        sharpened++;
        cardData = new UICardData("Forge", cost: 1, "Deal " + (4 + sharpened) + " damage, add a Great Sword to discard", UICardData.CardType.ATTACK);
    }
}
