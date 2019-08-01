using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trick : CardData
{
    public Trick()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Trick", cost: 2, "Stun an enemy. Gain 1 energy and 1 draw next turn", UICardData.CardType.SPELL, "Trick");
    }

    public override int GetBonusDamage()
    {
        return 0;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Stun();
        SoundManager.PlayCardSFX("Trick1");
        getDeckManager().AddDrawNextTurn();
        addEnergyNextTurn(1);
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
