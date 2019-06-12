﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energize : CardData
{
    public Energize()
    {
        target = Target.BOARD;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Energize", cost: 0, "Gain 1 Energy", UICardData.CardType.SPELL, "Mug");
    }

    public override void Action(EnemyManager[] enemys)
    {
        SoundManager.playSound("Energize1");
        addEnergy(1);
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
