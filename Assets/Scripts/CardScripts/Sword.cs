using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : CardData
{
    public Sword()
    {
        cardData = new UICardData("Sword", cost: 2, "Deal 3 damage.", UICardData.CardType.ATTACK, "Sword");
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        SoundManager.playSound("Sword1");
        enemys[0].Damage(3+sharpened);
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
        cardData = new UICardData("Sword", cost: 2, "Deal "+(3+sharpened)+ " damage.", UICardData.CardType.ATTACK);
    }
}

