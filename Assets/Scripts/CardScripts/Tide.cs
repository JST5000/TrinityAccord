using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tide : CardData
{
    private int growDamage = 0;
    public Tide()
    {
        target = Target.ALL_ENEMIES;
    }

    protected override UICardData CreateUICardData()
    {
         return new UICardData("Tide", cost: 3, "Deal " + GetDamage() + " damage to all enemies Grow 1", UICardData.CardType.ATTACK, "Tide");
    }

    private int GetDamage()
    {
        return 2 + growDamage + sharpenDamage;
    }


    public override void Action(EnemyManager[] enemys)
    {
        foreach(EnemyManager enemy in enemys)
        {
            enemy.Damage(GetDamage());

        }
        growDamage += 1;
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
