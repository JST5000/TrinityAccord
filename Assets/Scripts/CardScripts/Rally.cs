using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Rally : CardData
{
    private int growDamage = 0;

    public Rally()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Rally", cost: 2, "Deal " + GetDamage() + " damage Grow 2", UICardData.CardType.ATTACK);
    }

    private int GetDamage()
    {
        return 3 + growDamage + sharpened;
    }


    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(GetDamage());
        growDamage += 2;
        UpdateUICardData();
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
