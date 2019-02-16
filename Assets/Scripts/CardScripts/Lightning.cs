﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : CardData
{

    public Lightning()
    {
        //Using state since a card may be modified (Ex. Feather Blade changing cost)
        cardData = new UICardData("Lightning", cost: 3, "Deal 3 damage to 3 random enemies.", UICardData.CardType.ATTACK);
    }

    
    //Needs all enemies
    public override void Action(EnemyManager[] enemies)
    {
        int max = enemies.Length;
        int min = 0;
        //TODO requery for enemies after each hit, incase someone dies and you need to recalculate.
        //A new enemy may be spawned/removed based on a non-lightning effect so we cannot deduce from our set the new set of targets.
    }

    public override void Action(CardData[] cards)
    {
        throw new System.NotImplementedException("Lightning does not target cards");
    }

    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {
        throw new System.NotImplementedException("Lightning does not target cards and enemies at the same time.");
    }

    public override string CardName()
    {
        return cardData.cardName;
    }

    public override int Cost()
    {
        return cardData.cost;
    }

    public override Target Target()
    {
        return global::Target.ALL_ENEMIES;
    }
}