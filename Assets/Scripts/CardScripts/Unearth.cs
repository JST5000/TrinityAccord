using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unearth : CardData
{
    public Unearth()
    {
        target = Target.NONE;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Unearth", cost: 0, "Draw the top card from your discard.", UICardData.CardType.SPELL, "Unearth");
    }

    public override int GetBonusDamage()
    {
        return 0;
    }

    public override void Action(EnemyManager[] enemys)
    {
        DeckManager deckMan = DeckManager.Get();
        CardData card = deckMan.GrabTopCardOfDiscard();
        if (card != null && !deckMan.addCardToHand(card)) {
            //Reverse the change
            deckMan.AddToDiscard(card);
        }
        
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
