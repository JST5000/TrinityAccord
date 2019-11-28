using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apprentice : CardData
{
    public Apprentice()
    {
        target = Target.ENEMY;
    }

    //TODO 
    /*
     * Make cost disappear for cost below 0
     * Add change trigger
     * Change card effect
     *  Must account for Goldfish running away!
     *  Account for change of cost effects like One Ton Hammer
     * Change card art
     * Change card name
     */


    protected override UICardData CreateUICardData()
    {
        if (!Transformed)
        {
            return new UICardData("Apprentice", cost: 0, "Quest: Disarm all enemies once. (Becomes Master)", UICardData.CardType.QUEST);
        }
        else
        {
            return new UICardData("Master", cost: 0, "Disarm target enemy.", UICardData.CardType.SPELL);
        }
    }

    public override void Action(EnemyManager[] enemys)
    {
        if (Transformed)
        {
            enemys[0].Disarm();
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
