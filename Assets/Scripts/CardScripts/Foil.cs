using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foil : CardData
{
    private int growEnergy = 0;
    public Foil()
    {
        cardData = new UICardData("Foil", cost: 2, "Deal 3 damage, reduce cost by 1", UICardData.CardType.ATTACK);
        cost = 2;
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(3);
        growEnergy += 1;
        int newEnergy = 2 - growEnergy;
        if (newEnergy < 0)
        {
            newEnergy = 0;
        }
        cardData = new UICardData("Foil", cost: newEnergy, "Deal 3 damage, reduce cost by 1", UICardData.CardType.ATTACK);
        cost = newEnergy;

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
