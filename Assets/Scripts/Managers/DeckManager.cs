using System.Collections.Generic;
using System;
using UnityEngine;


public class DeckManager : MonoBehaviour
{
    public List<CardData> deck;
    public List<CardData> discard;
    public CardManager[] hand;

    //Used by Squirrel and Trick
    private int extraDrawsOnTurnStart = 0;

    //Initializes the player's deck
    void Start()
    {
        Init(PermanentState.playerDeck);  
    }

    public void Init(List<CardData> initDeck)
    {
        
        deck = new List<CardData>(initDeck);
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
        for(int i = 0; i < 4 + extraDrawsOnTurnStart; i++)
        {
            DrawCard();
        }
        extraDrawsOnTurnStart = 0;
    }

    public void EndTurn()
    {
        DiscardHand();
    }

    public CardData DrawCard()
    {
        return DrawAtIndex(0);
    }

    public CardData DrawAtIndex(int i)
    {
        if (deck.Count < i + 1)
        {
            ShuffleDiscardIntoDeck();
        }
        foreach (CardManager cardManager in hand)
        {
            if (cardManager.empty)
            {
                CardData toAdd = deck[i];
                cardManager.Init(deck[i]);
                deck.RemoveAt(i);
                GameObject.Find("Hand").GetComponent<HandManager>().UpdateAllCardsInHand();
                return toAdd;
            }
        }
        return null;

    }

    public void addCardToHand(CardData card)
    {
        foreach (CardManager cardManager in hand)
        {
            if (cardManager.empty)
            {
                cardManager.Init(card);
                GameObject.Find("Hand").GetComponent<HandManager>().UpdateAllCardsInHand();
                return;
            }
        }
    }
    public CardData getTop()
    {
        return GetAtIndex(0);
    }

    public CardData GetAtIndex(int i)
    {
        if (deck.Count < i + 1)
        {
            ShuffleDiscardIntoDeck();
        }
        Debug.Log("Deck Count: " + deck.Count + " i = " + i);
        if (deck.Count >= i + 1)
        {
            return deck[i];
        }
        return null;
    }

    public CardData grabTop()
    {
        if (deck.Count == 0)
        {
            ShuffleDiscardIntoDeck();
        }
        if (deck.Count != 0)
        {
            CardData toReturn = deck[0];
            deck.RemoveAt(0);
            return toReturn;
        }
        return null;
    }
    public CardData grabDiscard()
    {
        if (discard.Count == 0)
        {
            return null;
        }
        int randomIndex = UnityEngine.Random.Range(0, discard.Count);
        CardData toReturn = discard[randomIndex];
        discard.RemoveAt(randomIndex);
        return toReturn;


    }
    public void discardTop()
    {
        discard.Add(deck[0]);
        deck.RemoveAt(0);
       
    }
    public int getNumberOfCardsInHand()
    {
        int toReturn = 0;
        foreach(CardManager cardManager in hand)
        {
            if (!cardManager.IsEmpty())
            {
                toReturn++;
            }
        }
        return toReturn;
    }

    public void AddDrawNextTurn()
    {
        extraDrawsOnTurnStart++;
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
                cardMan.GetCardData().onDiscard();
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
    public GameObject getRandomCardTarget()
    {
        System.Random r = new System.Random();
        return hand[r.Next(0, hand.Length)].transform.gameObject;


    }





}
