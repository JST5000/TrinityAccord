using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockout : CardData
{
    public Knockout()
    {
        cardData = new UICardData("Knockout", cost: 2, "Deal 3 damage Stun", UICardData.CardType.ATTACK);
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(3+sharpened);
        enemys[0].Stun();
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
        cardData = new UICardData("Knockout", cost: 1, "Deal " + (3 + sharpened) + " damage Stun", UICardData.CardType.ATTACK);
    }
}