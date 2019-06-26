using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : CardData
{

    public Lightning()
    {
        target = Target.ALL_ENEMIES;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Lightning", cost: 3, "Deal " + GetDamage() + " damage to 3 random enemies.", UICardData.CardType.SPELL, "Lightning2");
    }

    private int GetDamage()
    {
        return 3;
    }

    //Needs all enemies
    public override void Action(EnemyManager[] enemies)
    {
        for (int i = 0; i < 3; ++i)
        {
            damageRandom(GetDamage());
        }
    }

    public override void Action(CardData[] cards)
    {
        throw new System.NotImplementedException("Lightning does not target cards");
    }

    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {
        throw new System.NotImplementedException("Lightning does not target cards and enemies at the same time.");
    }

    public override int SecondAction(CardManager card)
    {
        throw new System.NotImplementedException();
    }
}
