using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic : CardData
{
    public Relic()
    {
        target = Target.BOARD;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Relic", cost: 0, "Draw a card and Preserve it", UICardData.CardType.SPELL, "Relic");
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

}
