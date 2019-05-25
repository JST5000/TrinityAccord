using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : CardData
{
    public Cannon()
    {
        cardData = new UICardData("Cannon", cost: 3, "Deal 4 damage Deal 2 to adjacent", UICardData.CardType.ATTACK);
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        for(int i=0;i<getEnemyManagers().Length;i++)
        {
            if (getEnemyManagers()[i].Equals(enemys[0]))
            {
                getEnemyManagers()[i].Damage(4+sharpened);
                if (!(i - 1 < 0))
                {
                    getEnemyManagers()[i - 1].Damage(2+sharpened);
                }
                if (!(i + 1 >= getEnemyManagers().Length))
                {
                    getEnemyManagers()[i+1].Damage(2+sharpened);
                }

            }
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
    public override void sharpen()
    {
        sharpened++;
        cardData = new UICardData("Cannon", cost: 3, "Deal "+(4+sharpened)+ " damage Deal "+(2+sharpened)+" to adjacent", UICardData.CardType.ATTACK);
    }
}
