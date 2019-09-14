using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : CardData
{
    public Artifact()
    {
        target = Target.CARD;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Artifact", cost: 0, "Preserve target card", UICardData.CardType.SPELL, "Artifact");
    }

    public override int GetBonusDamage()
    {
        return 0;
    }

    public override void Action(EnemyManager[] enemys)
    {
        CardData draw = DeckManager.Get().DrawCard();
        CardManager man = HandManager.Get().GetManagerWithCard(draw);
        man.PreserveCard();
    }

    public override void Action(CardData[] cards)
    {
        HandManager.Get().GetManagerWithCard(cards[0]).PreserveCard();
    }

    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {
        throw new System.NotImplementedException();
    }

    public override int SecondAction(CardManager card)
    {
        throw new System.NotImplementedException();
    }

}
