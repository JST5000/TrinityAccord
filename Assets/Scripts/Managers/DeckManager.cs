using System.Collections.Generic;
using System;
using UnityEngine;


public class DeckManager : MonoBehaviour
{
    public List<CardData> deck;
    public List<CardData> discard;
    public CardManager[] hand;

    //Initializes the player's deck
    void Start()
    {
        Init(PermanentState.playerDeck);  
    }

    public void Init(List<CardData> initDeck)
    {
        
        deck = initDeck;
        discard = new List<CardData>();
        GameObject handObject = GameObject.Find("Hand");
        hand=handObject.GetComponentsInChildren<CardManager>();
        foreach (CardManager cardManager in hand)
        {
            cardManager.SetEmpty();
        }
        Shuffle(deck);
        StartTurn();
    }

    public void StartTurn()
    {
        for(int i = 0; i < 4; i++)
        {
            DrawCard();
        }
    }

    public void EndTurn()
    {
        DiscardHand();
    }

    public CardData DrawCard()
    {
        if(deck.Count == 0)
        {
            ShuffleDiscardIntoDeck();
        }
        foreach (CardManager cardManager in hand)
        {
            if (cardManager.empty)
            {
                CardData toAdd = deck[0];
                cardManager.Init(deck[0]);
                deck.RemoveAt(0);
                return toAdd;
            }
        }
        return null;

    }

    public void ShuffleDiscardIntoDeck()
    {
        deck.AddRange(discard);
        discard.RemoveRange(0, discard.Count);
        Shuffle(deck);
    }

    public void DiscardHand() {
        foreach(CardManager cardMan in hand)
        {
            if (!cardMan.IsEmpty())
            {
                discard.Add(cardMan.GetCardData());
                cardMan.SetEmpty();
            }
        }
    }

    void Shuffle(List<CardData> toShuffle)
    {
        System.Random r = new System.Random();
        List<CardData> toReturn = new List<CardData>();
        int randomIndex = 0;
        while (toShuffle.Count > 0)
        {
            randomIndex = r.Next(0, toShuffle.Count); //Choose a random object in the list
            toReturn.Add(toShuffle[randomIndex]); //add it to the new, random list
            toShuffle.RemoveAt(randomIndex); //remove to avoid duplicates
        }
        foreach (CardData card in toReturn)
        {
            toShuffle.Add(card);
        }
    }

    public void AddToDiscard(CardData card)
    {
        discard.Add(card);
    }

    public void PrintDeck()
    {
        Debug.Log("Deck Contains:");
        Print(deck);
        Debug.Log("---- End of List ----");
    }

    public void PrintDiscard()
    {
        Debug.Log("Discard Contains:");
        Print(discard);
        Debug.Log("---- End of List ----");
    }

    void Print(List<CardData> pile)
    {
        foreach(CardData card in pile)
        {
            Debug.Log(card);
        }
    }

    

}
