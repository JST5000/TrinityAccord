﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Peer : CardData
{
    CardData[] topCards = { null, null };

    public Peer()
    {
        cardData = new UICardData("Peer", cost: 0, "Look at the top 2 cards of the deck and draw one", UICardData.CardType.SPELL);
        cost = 0;
        target = Target.BOARD;
    }
    
    public override void Action(EnemyManager[] enemys)
    {
        GameObject instance = GameObject.Instantiate(Resources.Load<GameObject>("Choose3Menu"), GameObject.Find("Canvas").transform, false);
        Choose3Manager choose3 = instance.GetComponent<Choose3Manager>();
        DeckManager deck = GameObject.Find("Deck").GetComponent<DeckManager>();
        
        //Forces shuffle at start of effect instead of midway
        topCards[1] = deck.GetAtIndex(1);
        topCards[0] = deck.GetAtIndex(0);
        choose3.Init(topCards);

        //VERY IMPORTANT! 
        //This tells the choose 3 to send the decision to this card by caling the Action(CardData[] cards) function!
        choose3.SendDecisionTo(this);
    }

    //Will be called by the Choose3Manager when a choice has been made
    public override void Action(CardData[] cards)
    {
        DeckManager deck = GameObject.Find("Deck").GetComponent<DeckManager>();
        if (cards[0] == topCards[0])
        {
            deck.DrawAtIndex(0);
        } else
        {
            deck.DrawAtIndex(1);
        }
    }

    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {

    }

    public override int SecondAction(CardManager card)
    {
        throw new System.NotImplementedException();
    }
}
