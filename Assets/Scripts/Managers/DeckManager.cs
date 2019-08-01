using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class DeckManager : MonoBehaviour
{
    public TextMeshProUGUI deckCount;
    public TextMeshProUGUI discardCount;

    public List<CardData> deck;
    public List<CardData> discard;
    public CardManager[] hand;

    //Used by Squirrel and Trick
    private int extraDrawsOnTurnStart = 0;

    public Transform DiscardView;

    public static DeckManager Get()
    {
        return GameObject.Find("Deck").GetComponent<DeckManager>();
    }

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
        hand = GameObject.Find("Hand").GetComponentsInChildren<CardManager>();
        for (int i = 0; i < 4 + extraDrawsOnTurnStart; i++)
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
        CardData toAdd = deck[i];
        if(addCardToHand(toAdd))
        {
            deck.RemoveAt(i);
            return toAdd;
        }
        return null;
    }

    //Returns true if able to add the card
    public bool addCardToHand(CardData card)
    {
        foreach (CardManager cardManager in hand)
        {
            if (cardManager.empty)
            {
                card.OnDraw();
                cardManager.Init(card);
                HandManager.Get().UpdateAllCardsInHand();
                return true;
            }
        }
        return false;
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
    public CardData grabDiscard(int index = -1)
    {
        if (discard.Count == 0)
        {
            return null;
        }

        int toReturnIndex = index;

        if (index < 0)
        {
            toReturnIndex = UnityEngine.Random.Range(0, discard.Count);
        }

        CardData toReturn = discard[toReturnIndex];
        discard.RemoveAt(toReturnIndex);
        return toReturn;


    }

    public CardData GrabTopCardOfDiscard()
    {
        return grabDiscard(discard.Count - 1);
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
            if (!cardMan.IsEmpty() && !cardMan.IsPreserved())
            {
                cardMan.GetCardData().OnDiscard();
                discard.Add(cardMan.GetCardData());
                cardMan.SetEmpty();
            }
            cardMan.DisablePreserved();
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

    public void DisplayDiscardToPlayer()
    {
        Debug.Log(DiscardView);
        if(DiscardView == null)
        {
            DiscardView = Instantiate(Resources.Load<Transform>("Prefabs/CardViewer1"), GameObject.Find("Canvas").transform, false);

        }
        Debug.Log(DiscardView);
        DiscardView.localScale = new Vector3(1, 1, 1);
        RectTransform canvasRect = (RectTransform)transform.parent;
        DiscardView.GetComponent<CardViewerManager>().Init(discard.ToArray(), startOnLeft: false);
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

    public CardManager GetRandomValidCardManagerFromHand()
    {
        List<int> validCards = new List<int>();
        for (int i = 0; i < hand.Length; ++i)
        {
            if (!hand[i].IsEmpty())
            {
                validCards.Add(i);
            }
        }
        //No cards in hand
        if (validCards.Count == 0)
        {
            return null;
        }
        int randomIndex = UnityEngine.Random.Range(0, validCards.Count);
        return hand[validCards[randomIndex]];
    }

    private void Update()
    {
        deckCount.text = "(" + deck.Count + ")";
        discardCount.text = "(" + discard.Count + ")";
    }
}
