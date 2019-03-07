using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quickdraw : CardData
{
    public Quickdraw()
    {
        cardData = new UICardData("Quickdraw", cost: 1, "Discard hand, draw 3", UICardData.CardType.SPELL);
        cost = 1;
        target = Target.BOARD;
    }

    public override void Action(EnemyManager[] enemys)
    {
        GameObject.Find("Deck").GetComponent<DeckManager>().DiscardHand();
        draw();
        draw();
        draw();
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
}
