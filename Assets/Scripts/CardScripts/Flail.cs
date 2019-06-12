using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flail : CardData
{
    public Flail()
    {
        target = Target.BOARD;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Flail", cost: 1, "Deal " + GetDamage() + " damage to random enemy", UICardData.CardType.ATTACK);
    }

    private int GetDamage()
    {
        return 3 + sharpened;
    }

    public override void Action(EnemyManager[] enemies)
    {
        damageRandom(3+sharpened);
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
