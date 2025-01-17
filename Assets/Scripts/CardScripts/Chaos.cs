﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaos : CardData
{

    public Chaos()
    {
        target = Target.ALL_ENEMIES;
    }

    protected override UICardData CreateUICardData()
    {
        string effect = "Deal 1 damage at random for each card played this turn (including this)";
        if (GetBonusDamage() > 0)
        {
            effect += " (+" + GetBonusDamage() + ")";
        }
        return new UICardData("Chaos", cost: 1, effect, UICardData.CardType.ATTACK, "Chaos_mastery");
    }

    public override void Action(EnemyManager[] enemies)
    {
        for (int i = 0; i < getNumberOfCardsPlayed() + GetBonusDamage(); ++i)
        {
            damageRandom(1);
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
