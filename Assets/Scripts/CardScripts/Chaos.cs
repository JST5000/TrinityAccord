using System.Collections;
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
        string effect = "Deal 1 damage at random for each card played before this";
        if (sharpened > 0)
        {
            effect += " (+" + sharpened + ")";
        }
        return new UICardData("Chaos", cost: 1, effect, UICardData.CardType.ATTACK);
    }

    public override void Action(EnemyManager[] enemies)
    {
        for (int i = 0; i < getNumberOfCardsPlayed() - 1 + sharpened; ++i)
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
