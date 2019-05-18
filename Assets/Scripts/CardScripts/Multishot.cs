using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multishot : CardData
{
    private int growDamage = 0;
    private static int growthRate = 3;
    private static int baseDamage = 3;

    public Multishot() : base(GetBaselineUICardData(), Target.ENEMY)
    {
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(3 + growDamage+sharpened);
        growDamage = 0;
        if (sharpened == 0)
        {
            cardData = new UICardData("Multishot", cost: 2, "Deal 3 damage Charge 3", UICardData.CardType.ATTACK);
        }
        else
        {
            cardData = new UICardData("Multishot", cost: 2, "Deal " + (3 + sharpened) + " damage Charge 3", UICardData.CardType.ATTACK);
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
    public override void onDiscard()
    {
        growDamage += growthRate;
        cardData = GetUICardData();

    }

    
    public UICardData GetUpdatedUIData()
    {
        return new UICardData("Multishot", cost: 2, "Deal " + (baseDamage + growDamage + sharpened) + " damage Charge " + growthRate, UICardData.CardType.ATTACK);
    }

    private static UICardData GetBaselineUICardData()
    {
        return new UICardData("Multishot", cost: 2, "Deal " + baseDamage + " damage Charge " + growthRate, UICardData.CardType.ATTACK);
    }

    public override void sharpen()
    {
        sharpened++;
        cardData = GetUpdatedUIData();
    }
}
