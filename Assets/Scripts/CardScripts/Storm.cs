using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : CardData
{
    private int chargeDamage = 0;
    public Storm()
    {
        target = Target.ALL_ENEMIES;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Storm", cost: 3, "Deal " + GetDamage() + " damage to all enemies Charge 1", UICardData.CardType.ATTACK, "Storm");
    }

    public override int GetBonusDamage()
    {
        return chargeDamage + base.GetBonusDamage();
    }

    private int GetDamage()
    {
        return 2 + GetBonusDamage();
    }

    public override void Action(EnemyManager[] enemys)
    {
        foreach (EnemyManager enemy in enemys)
        {
            enemy.Damage(GetDamage());

        }
        chargeDamage = 0;
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
    public override void OnDiscard()
    {
        chargeDamage += 1;
        UpdateUICardData();
    }
}
