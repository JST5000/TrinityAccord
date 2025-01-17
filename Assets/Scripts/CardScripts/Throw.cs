﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : CardData
{
    public Throw()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Throw", cost: 2, "Deal " + GetPrimaryDamage() + " damage. Destroy top card of deck. If its an attack, +" + GetAdditionalDamage() + " damage", UICardData.CardType.ATTACK);
    }

    private int GetPrimaryDamage()
    {
        return 3 + GetBonusDamage();
    }

    private int GetAdditionalDamage()
    {
        return 2 + GetBonusDamage();
    }

    public override void Action(EnemyManager[] enemys)
    {
        CardData top = BurnTopCard();
        if (top==null)
        {
            return;
        }
        int totalDamage = GetPrimaryDamage();
        if (top.GetTypeOfCard().Equals(UICardData.CardType.ATTACK))
        {
            totalDamage += GetAdditionalDamage();
        }
        enemys[0].Damage(totalDamage);

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
