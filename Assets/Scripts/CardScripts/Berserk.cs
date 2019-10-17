using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserk : CardData
{
    public Berserk()
    {
        target = Target.NONE;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Berserk", cost: 3, "Play top 3 cards of deck at random", UICardData.CardType.SPELL, "Berserk");
    }

    public override int GetBonusDamage()
    {
        return 0;
    }

    public override void Action(EnemyManager[] enemys)
    {
        for (int i = 0; i < 3; i++)
        {
            CardData top = grabTop();
            if (top == null)
            {
                return;
            }
            playCardRandomTarget(top);
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

