using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Peer : CardData
{
    CardData[] topCards = { null, null };

    public Peer()
    {
        pauseGameplay = true;
        target = Target.BOARD;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Peer", cost: 0, "Look at the top 2 cards of the deck and draw one", UICardData.CardType.SPELL, "Peer");
    }

    public override int GetBonusDamage()
    {
        return 0;
    }

    public override void Action(EnemyManager[] enemys)
    {
        //Prevent end turn during card selection
        PauseExecutionUntilChoiceMade();

        GameObject instance = GameObject.Instantiate(Resources.Load<GameObject>("Choose3Menu"), GameObject.Find("Canvas").transform, false);
        Choose3Manager choose3 = instance.GetComponent<Choose3Manager>();
        DeckManager deck = DeckManager.Get();
        
        //Forces shuffle at start of effect instead of midway
        topCards[1] = deck.GetAtIndex(1);
        topCards[0] = deck.GetAtIndex(0);
        choose3.Init(topCards);

        //VERY IMPORTANT! 
        //This tells the choose 3 to send the decision to this card by caling the Action(CardData[] cards) function!
        choose3.SendDecisionTo(this);
        //Allows the menu to be hidden so the players can look at their cards when deciding.
        choose3.AllowHide();
    }

    //Will be called by the Choose3Manager when a choice has been made
    public override void Action(CardData[] cards)
    {
        DeckManager deck = DeckManager.Get();
        if (cards[0] == topCards[0])
        {
            deck.DrawAtIndex(0);
        } else
        {
            deck.DrawAtIndex(1);
        }

        //Must be resumed explicitly since we flagged as a 'PauseGameplay' card
        ResumeExecution();
    }

    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {

    }

    public override int SecondAction(CardManager card)
    {
        throw new System.NotImplementedException();
    }
}
