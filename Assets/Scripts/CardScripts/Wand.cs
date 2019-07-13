using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : CardData
{
    public Wand()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Wand", cost: 1, "Deal " + GetDamage() + " damage. Play the top card of deck if it is a spell", UICardData.CardType.ATTACK);
    }

    private int GetDamage()
    {
        return 2 + sharpenDamage;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(GetDamage());
        CardData top=grabTop();
        if (top==null)
        {
            return;
        }
        if (top.getType().Equals(UICardData.CardType.SPELL))
        {
            Debug.Log("Name of wanded card is: " + top.getName());
            playCardSameTarget(top);
        }
        else
        {
            DiscardCardOnStack(top);
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


