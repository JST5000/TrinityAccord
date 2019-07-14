using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yawn : CardData
{
    public Yawn()
    {
        target = Target.BOARD;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Yawn", cost: 1, "Drowsy a random enemy", UICardData.CardType.SPELL);
    }

    public override int GetBonusDamage()
    {
        return 0;
    }

    public override void Action(EnemyManager[] enemies)
    {
        GetRandomEnemy().Drowsy();
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

