using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovalItem : ShopItem
{
    public RemovalItem(int cost = 3) : base(name: "Remove Dagger", cost, "DaggerRemoval") { }

    override public void Effect()
    {
        string name = new Dagger().getName();
        if (PermanentState.playerDeck != null)
        {
            foreach (CardData card in PermanentState.playerDeck)
            {
                if (card.getName().Equals(name))
                {
                    PermanentState.playerDeck.Remove(card);
                    return;
                }
            }
        }
    }
}
