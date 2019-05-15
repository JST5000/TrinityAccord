using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : CardData
{
    public Hammer()
    {
        cardData = new UICardData("Hammer", cost: 2, "Deal 1-7 damage at random", UICardData.CardType.ATTACK);
        cost = 2;
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(Random.Range(1+sharpened, 7+sharpened));
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
        cardData = new UICardData("Hammer", cost: 2, "Deal "+(1+sharpened)+"-"+(7+sharpened)+" damage at random", UICardData.CardType.ATTACK);
    }
}
