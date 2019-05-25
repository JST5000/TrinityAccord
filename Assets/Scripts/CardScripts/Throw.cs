using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : CardData
{
    public Throw()
    {
        cardData = new UICardData("Throw", cost: 2, "Deal 3 damage. Destroy top card of deck. If its an attack, +2 damage", UICardData.CardType.ATTACK);
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(3+sharpened);
        CardData top = grabTop();
        if (top==null)
        {
            return;
        }
        if (top.getType().Equals(UICardData.CardType.ATTACK))
        {
            enemys[0].Damage(2);
        }
        getDeckManager().grabTop();
        
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
    public override void sharpen()
    {
        sharpened++;
        cardData = new UICardData("Throw", cost: 2, "Deal " + (1 + sharpened) + " damage. Destroy top card of deck. If its an attack, +2 damage", UICardData.CardType.ATTACK);
    }
}
