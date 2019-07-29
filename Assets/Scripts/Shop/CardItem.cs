using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardItem : ShopItem {

    public CardItem(CardData card, int cost = 2) : base(card.getName(), cost, card, true)
    {
        this.card = CardDataUtil.CreateFreshCopiesOf(card);
    }

    public override void Effect()
    {
        PermanentState.playerDeck.Add(this.card);
    }
}
