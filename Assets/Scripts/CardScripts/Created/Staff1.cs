using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff1 : CardData
{
    public Staff1()
    {
        cardData = new UICardData("A", cost: 0, "Deal 2 damage", UICardData.CardType.ATTACK);
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
        cardData = new UICardData("A", cost: 0, "Deal " + (2 + sharpened) + " damage.", UICardData.CardType.ATTACK);
    }
}
