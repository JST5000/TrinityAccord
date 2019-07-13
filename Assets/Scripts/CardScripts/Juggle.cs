﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juggle : CardData
{
    public Juggle()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Juggle", cost: 1, "If there is spell in hand, deal " + GetDamage() + " damage and stun an enemy", UICardData.CardType.ATTACK, cardArtFileName: "Juggle");
    }

    private int GetDamage()
    {
        return 3 + sharpenDamage;
    }


    public override void Action(EnemyManager[] enemys)
    {
        SoundManager.playSound("Juggle1");
        enemys[0].Damage(GetDamage());
        enemys[0].Stun();
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
    public override bool IsPlayableAdditionalRequirements()
    {

        foreach (CardManager cardManager in getHand())
        {
                if (!cardManager.IsEmpty() && cardManager.GetCardData()!=null&&cardManager.GetCardData().getType().Equals(UICardData.CardType.SPELL))
                    return true;
        }
        return false;
        
    }
}
