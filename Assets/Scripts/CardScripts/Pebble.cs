using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pebble : CardData
{
    public Pebble()
    {
        cardData = new UICardData("Pebble", cost: 0, "Deal 1 damage Draw 1 card", UICardData.CardType.ATTACK, "Pebble");
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(1+sharpened);
        draw();
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
        cardData = new UICardData("Pebble", cost: 0, "Deal " + (1 + sharpened) + " damage Draw 1 card", UICardData.CardType.ATTACK);
    }
}
