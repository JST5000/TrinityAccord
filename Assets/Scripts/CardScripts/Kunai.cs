using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : CardData
{
    public Kunai()
    {
        cardData = new UICardData("Kunai", cost: 2, "Deal 3 damage, add a shuriken to discard", UICardData.CardType.ATTACK);
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(3+sharpened);
        addCardToDiscard(new Shuriken());

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
        cardData = new UICardData("Kunai", cost: 1, "Deal " + (3 + sharpened) + " damage, add a shuriken to discard", UICardData.CardType.ATTACK);
    }
}
