﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : CardData
{
    bool isReloading;

    public Crossbow()
    {
        isReloading = false;
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        if(isReloading)
        {
            return new UICardData("Reload", cost: 0, "Flip Crossbow", UICardData.CardType.SPELL);
        } else
        {
            return new UICardData("Crossbow", cost: 2, "Deal " + GetDamage() + " damage Flip Reload", UICardData.CardType.ATTACK);
        }
    }

    public override int GetBonusDamage()
    {
        if (isReloading)
        {
            return 0;
        } else
        {
            return base.GetBonusDamage();
        }
    }

    private int GetDamage()
    {
        return 6 + GetBonusDamage();
    }

    private void ToggleReload()
    {
        isReloading = !isReloading;
        if(isReloading)
        {
            target = Target.NONE;
        } else
        {
            target = Target.ENEMY;
        }
        UpdateUICardData(doNotUpdateCost: false);
    }

    public override void Action(EnemyManager[] enemys)
    {
        if (!isReloading)
        {
            enemys[0].Damage(GetDamage());
        }
        ToggleReload();
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
