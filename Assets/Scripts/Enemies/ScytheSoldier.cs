using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheSoldier : EnemyData
{
    public ScytheSoldier()
        : base(name: "Scythe", maxHP: 6, staggers: 2, damage: 2, timer: 2, effect: InLineIcon.DAMAGE + ": 2, Destroy a random card played this turn.", spriteName: "Spirit", "Scythe Soldier")
    { }

    //TODO add ", Destroy a card played this turn." to the attack text when the code is working ^^^^

    //TODO
    protected override void AttackUniqueEffect()
    {
        StackManager stack = GameObject.Find("StackHolder").GetComponent<StackManager>();
        CardData recentlyPlayed = stack.GetRandomCardPlayedThisTurn();
        if (recentlyPlayed != null)
        {
            CardData removedCard = null;
            DeckManager deckMan = DeckManager.Get();
            int discardIndex = CardDataUtil.FindCard(deckMan.discard, recentlyPlayed);
            if (discardIndex != -1)
            {
                removedCard = deckMan.discard[discardIndex];
                deckMan.discard.RemoveAt(discardIndex);
            }
            else
            {
                int deckIndex = CardDataUtil.FindCard(deckMan.deck, recentlyPlayed);
                if (deckIndex != -1)
                {

                    removedCard = deckMan.deck[deckIndex].CloneCardType();
                    deckMan.deck.RemoveAt(deckIndex);
                }
            }

            if (removedCard != null)
            {
                //Shows the player what was lost
                StackManager playStack = GameObject.Find("StackHolder").GetComponent<StackManager>();
                playStack.Push(removedCard, StackUsage.DESTROY);
            }
        }
    }
}
