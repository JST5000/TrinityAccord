using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : CardData
{
    public Assassin()
    {
        target = Target.NONE;
    }

    public override bool Transformed
    {
        get => base.Transformed;
        set
        {
            base.Transformed = value;

            //This is where the real target is set
            target = Target.ENEMY;
        }
    }

    protected override UICardData CreateUICardData()
    {
        if (!Transformed)
        {
            return new UICardData("Assassin", cost: 0, "Draw a card.\nQuest: Kill an enemy. (Becomes Mark)", UICardData.CardType.QUEST);
        }
        else
        {
            return new UICardData("Mark", cost: 0, "Give target enemy Vulnerable for one turn.", UICardData.CardType.SPELL);
        }
    }

    public override void Action(EnemyManager[] enemys)
    {
        if (Transformed)
        {
            enemys[0].GetEnemyData().Vulnerable = true;
        } else
        {
            draw();
        }
    }

    public override void Action(CardData[] cards)
    {
        throw new System.NotImplementedException();
    }

    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {
        throw new System.NotImplementedException();
    }

    public override int SecondAction(CardManager card)
    {
        throw new System.NotImplementedException();
    }
}
