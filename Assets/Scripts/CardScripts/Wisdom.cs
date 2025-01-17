﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisdom : CardData
{
    public Wisdom()
    {
        target = Target.NONE;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Wisdom", cost: 1, "Draw 1 from discard, draw 1 ", UICardData.CardType.SPELL);
    }

    public override int GetBonusDamage()
    {
        return 0;
    }

    public override void Action(EnemyManager[] enemys)
    {
       
        drawFromDiscard();
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
