﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idea : CardData
{
    public Idea()
    {
        cardData = new UICardData("Idea", cost: 0, "Draw 2", UICardData.CardType.SPELL);
        cost = 0;
        target = Target.BOARD;
    }

    public override void Action(EnemyManager[] enemys)
    {
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