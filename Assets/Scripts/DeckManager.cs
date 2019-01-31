using System.Collections;
using System;
using UnityEngine;


public class DeckManager : MonoBehaviour
{
    public ArrayList deck;
    public ArrayList discard;
    public CardManager[] hand;

    public void Init(ArrayList initDeck)
    {
        
        deck = initDeck;
        discard = new ArrayList();
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
    public void DrawCard()
    {
        foreach (CardManager cardManager in hand)
        {
            if (cardManager.empty)
            {
                cardManager.Init((CardData)deck[0]);
                Debug.Log(((CardData)deck[0]).CardName());
                deck.RemoveAt(0);
                return;
            }
        }
    }
    void Shuffle(ArrayList toShuffle)
    {
        System.Random r = new System.Random();
        ArrayList toReturn = new ArrayList();
        int randomIndex = 0;
        while (toShuffle.Count > 0)
        {
            randomIndex = r.Next(0, toShuffle.Count); //Choose a random object in the list
            toReturn.Add(toShuffle[randomIndex]); //add it to the new, random list
            toShuffle.RemoveAt(randomIndex); //remove to avoid duplicates
        }
        foreach (System.Object o in toReturn)
        {
            toShuffle.Add(o);
        }
    }

}
