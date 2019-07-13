using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multishot : CardData
{
    private int chargeDamage = 0;
    private static int growthRate = 3;
    private static int baseDamage = 3;

    public Multishot()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Multishot", cost: 2, "Deal " + GetDamage() + " damage Charge 3", UICardData.CardType.ATTACK);
    }

    private int GetDamage()
    {
        return 3 + chargeDamage + sharpenDamage;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(GetDamage());
        chargeDamage = 0;
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
        uiCardData = GetUICardData();

    }
}
