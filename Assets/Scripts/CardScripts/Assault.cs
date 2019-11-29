using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assault : CardData
{
    public int AttacksToTrigger = 10;
    private int draws = 3;

    public override bool Transformed { get => base.Transformed;
        set
        {
            base.Transformed = value;
            
            //This is where the real target is set
            target = Target.NONE;
        }
    }

    public Assault()
    {
        target = Target.NONE;
    }

    protected override UICardData CreateUICardData()
    {
        //TODO - Make this work for when drafting after a fight. Should not have the (X More!) text in that case.
        if (!Transformed)
        {
            StackManager stackMan = StackManager.Get();
            int playedSoFar = stackMan ? stackMan.AttacksPlayedThisEncounter : 0;
            int amountToGo = AttacksToTrigger - playedSoFar;
            return new UICardData("Assault", cost: 0, $"Draw a card.\nQuest: Play {AttacksToTrigger} Attacks\n({amountToGo} more!)\n(Becomes Follow Up)", UICardData.CardType.QUEST);
        }
        else
        {
            return new UICardData("Follow Up", cost: 0, $"Draw {draws}", UICardData.CardType.SPELL);
        }
    }

    public override void Action(EnemyManager[] enemys)
    {
        if (Transformed)
        {
            for (int i = 0; i < draws; ++i)
            {
                draw();
            }
        }
        else
        {
            draw();
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
