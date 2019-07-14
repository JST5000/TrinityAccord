using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : CardData
{
    public Forge()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Forge", cost: 3, "Deal " + GetDamage() + " damage, add a Great Sword to discard", UICardData.CardType.ATTACK);
    }

    private int GetDamage()
    {
        return 4 + GetBonusDamage();
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(GetDamage());
        addCardToDiscard(new GreatSword());

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
