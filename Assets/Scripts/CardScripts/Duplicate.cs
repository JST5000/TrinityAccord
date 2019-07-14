using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicate : CardData
{
    public Duplicate()
    {
        target = Target.BOARD;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Duplicate", cost: 1, "Copy the next spell played", UICardData.CardType.SPELL);
    }

    public override int GetBonusDamage()
    {
        return 0;
    }

    public override void Action(EnemyManager[] enemys)
    {
        GameObject.Find("StackHolder").GetComponent<StackManager>().duplicate++;
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
