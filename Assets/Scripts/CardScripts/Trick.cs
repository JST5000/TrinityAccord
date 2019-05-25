using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trick : CardData
{
    public Trick()
    {
        cardData = new UICardData("Trick", cost: 2, "Stun an enemy. Gain 1 energy 1 draw next turn", UICardData.CardType.SPELL);
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Stun();
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
