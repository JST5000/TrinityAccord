using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff2 : CardData
{
    public Staff2()
    {
        cardData = new UICardData("B", cost: 0, "Deal 1 damage Draw 1 card", UICardData.CardType.ATTACK);
    }

    public override void Action(EnemyManager[] enemys)
    {
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
        cardData = new UICardData("B", cost: 0, "Deal " + (1 + sharpened) + " damage Draw 1 card", UICardData.CardType.ATTACK);
    }
}
