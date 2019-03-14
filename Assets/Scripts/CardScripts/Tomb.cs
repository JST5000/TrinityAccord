using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomb : CardData
{
    public Tomb()
    {
        cardData = new UICardData("Tomb", cost: 1, "Draw 2, Gain 1 energy for each spell drawn", UICardData.CardType.SPELL);
        cost = 1;
        target = Target.BOARD;
    }

    public override void Action(EnemyManager[] enemys)
    {
        for (int i = 0; i < 2; i++)
        {
            CardData drawnCard = draw();
            if(drawnCard!=null)
            if (drawnCard.getType().Equals(UICardData.CardType.SPELL))
            {
                addEnergy(1);
            }
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
}
