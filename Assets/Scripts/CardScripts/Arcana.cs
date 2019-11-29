using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arcana : CardData
{
    public int SpellsToCompleteQuest { get; set; } = 3;

    private int CostOfSpellsThisTurn = 0;

    public Arcana()
    {
        target = Target.NONE;
    }

    protected override UICardData CreateUICardData()
    {
        if (!Transformed)
        {
            StackManager stackMan = StackManager.Get();
            int playedSoFar = stackMan ? stackMan.SpellsPlayedThisEncounter : 0;
            int amountToGo = SpellsToCompleteQuest - playedSoFar;
            return new UICardData("Arcana", cost: 0, $"Draw a card.\nQuest: Play {SpellsToCompleteQuest} spells.\n({amountToGo} more!)\n(Becomes Mana Well)", UICardData.CardType.QUEST);
        }
        else
        {
            return new UICardData("Mana Well", cost: 0, $"All spells cost {CostOfSpellsThisTurn} this turn.", UICardData.CardType.SPELL);
        }
    }

    public override void Action(EnemyManager[] enemys)
    {
        if (Transformed)
        {
            //This is returned to -1 by the DeckManager at the end of each turn.
            FixedCostForSpells = CostOfSpellsThisTurn;
        }
        else
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
