using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheSoldier : EnemyData
{
    public ScytheSoldier()
        : base(name: "Scythe", maxHP: 6, staggers: 2, damage: 2, timer: 2, effect: InLineIcon.DAMAGE + ": 2, Destroy a card played this turn.", spriteName: "Spirit", "Scythe Soldier")
    { }

    //TODO
    protected override void AttackUniqueEffect()
    {
        StackManager stack = GameObject.Find("StackHolder").GetComponent<StackManager>();
        CardData recentlyPlayed = stack.GetRandomCardPlayedThisTurn();
        if(recentlyPlayed != null)
        {
            DeckManager deckMan = GameObject.Find("Deck").GetComponent<DeckManager>();
            int discardIndex = CardDataUtil.FindCard(deckMan.discard, recentlyPlayed);
            if (discardIndex != -1)
            {
                deckMan.discard.RemoveAt(discardIndex);
            }
            else
            {
                int deckIndex = CardDataUtil.FindCard(deckMan.deck, recentlyPlayed);
                if (deckIndex != -1)
                {
                    deckMan.deck.RemoveAt(deckIndex);
                }
                else
                {
                    Debug.LogError(CardDataUtil.FindCard(new List<CardManager>(deckMan.hand), recentlyPlayed));
                    Debug.LogError("Tried to remove :" + recentlyPlayed + ", but was unable to find it in the Deck or Discard! Scythe Soldier attack has failed.");
                }
            }
        }
    }
}
