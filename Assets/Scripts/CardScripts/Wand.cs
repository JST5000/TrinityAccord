using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : CardData
{
    public Wand()
    {
        cardData = new UICardData("Wand", cost: 1, "Deal 2 damage. Wand top card of deck", UICardData.CardType.ATTACK);
        cost = 1;
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(2);
        CardData top=grabTop();
        if (top.Equals(null))
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
            addCardToDiscard(top);
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


