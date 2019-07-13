using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : CardData
{
    public Cannon()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Cannon", cost: 3, "Deal " + GetCenterDamage() +" damage Deal " + GetOuterDamage() + " to adjacent", UICardData.CardType.ATTACK);
    }

    private int GetCenterDamage()
    {
        return 4 + sharpenDamage;
    }

    private int GetOuterDamage()
    {
        return 2 + sharpenDamage;
    }

    public override void Action(EnemyManager[] enemys)
    {
        for(int i=0;i<getEnemyManagers().Length;i++)
        {
            if (getEnemyManagers()[i].Equals(enemys[0]))
            {
                getEnemyManagers()[i].Damage(GetCenterDamage());
                if (!(i - 1 < 0))
                {
                    getEnemyManagers()[i - 1].Damage(GetOuterDamage());
                }
                if (!(i + 1 >= getEnemyManagers().Length))
                {
                    getEnemyManagers()[i+1].Damage(GetOuterDamage());
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
}
