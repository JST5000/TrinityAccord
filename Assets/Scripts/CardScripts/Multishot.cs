using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multishot : CardData
{
    private int chargeDamage = 0;
    private static int growthRate = 3;

    public Multishot()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Multishot", cost: 2, "Deal " + GetDamage() + " damage Charge 3", UICardData.CardType.ATTACK);
    }

    public override int GetBonusDamage()
    {
        return base.GetBonusDamage() + chargeDamage;
    }

    private int GetDamage()
    {
        return 3 + GetBonusDamage();
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(GetDamage());
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
        chargeDamage += growthRate;
        UpdateUICardData();
    }
}
