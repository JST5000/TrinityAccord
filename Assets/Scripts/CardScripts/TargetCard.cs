using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCard : CardData
{
    public TargetCard()
    {
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(GetDamage());
        EncounterManager encounter = GameObject.Find("Board").GetComponent<EncounterManager>();
        encounter.SetTargetedEnemy(enemys[0]);
    }

    public override void Action(CardData[] cards)
    {
        throw new System.NotImplementedException();
    }

    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {
        throw new System.NotImplementedException();
    }

    public override int SecondAction(CardManager card)
    {
        throw new System.NotImplementedException();
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Target", cost: 0, "Deal " + GetDamage() + " damage. All random targets will hit this enemy this turn.", UICardData.CardType.SPELL);
    }

    private int GetDamage()
    {
        return 1 + sharpenDamage;
    }
}
