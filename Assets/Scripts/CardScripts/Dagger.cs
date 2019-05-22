using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : CardData
{
    public Dagger()
    {
        cardData = new UICardData("Dagger", cost: 1, "Deal 1 damage.", UICardData.CardType.ATTACK);
        cost = 1;
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        SoundManager.playSound("Sounds/Knife1");
        enemys[0].Damage(1+sharpened);
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
        cardData = new UICardData("Dagger", cost: 1, "Deal " + (1 + sharpened) + " damage.", UICardData.CardType.ATTACK);
    }
}
