﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tide : CardData
{
    private int growDamage = 0;
    public Tide()
    {
        cardData = new UICardData("Tide", cost: 3, "Deal 2 damage to all enemies Grow 1", UICardData.CardType.ATTACK);
        cost = 3;
        target = Target.ALL_ENEMIES;
    }

    public override void Action(EnemyManager[] enemys)
    {
        foreach(EnemyManager enemy in enemys)
        {
            enemy.Damage(2 + growDamage);

        }
        growDamage += 1;
        cardData = new UICardData("Tide", cost: 3, "Deal " + (2 + growDamage) + " damage to all enemies Grow 1", UICardData.CardType.ATTACK);

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