using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardDataUtil 
{
    public static Dictionary<string, CardData> nameToCard;

    public static Dictionary<string, CardData> GetNameDictionary(CardData[] allCards)
    {
        Dictionary<string, CardData> nToC = new Dictionary<string, CardData>();
        for (int i = 0; i < allCards.Length; ++i)
        {
            nToC[allCards[i].getName()] = allCards[i];
        }
        return nToC;
    }

    public static CardData InterpretWord(string CardName)
    {
        if (nameToCard == null)
        {
            nameToCard = GetNameDictionary(CreateFreshCopiesOf(CardPools.GetAllCardsIncludingDefaults()).ToArray());
        }

        return nameToCard[CardName].CloneCardType();
    }

    //Throws KeyNotFoundException if input is invalid. 
    public static CardData[] InterpretText(string input)
    {
        if(input == null || input == "")
        {
            throw new KeyNotFoundException("Unable to find empty input");
        }

        Debug.Log("Text being interpretted: " + input);
        string[] separator = { ", " };
        string[] split = input.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
        List<CardData> cards = new List<CardData>();
        foreach (string CardName in split)
        {
            /* Useful debug info
             * for(int i = 0; i < CardName.Length; ++i)
            {
                Debug.Log(CardName[i]);
            } */
            //Debug.Log("Split Text: " + CardName);
            if (CardName != null)
            {
                CardData result = InterpretWord(CardName.Trim());
                Debug.Log("Card Interpretted: " + result.getName());
                cards.Add(result);
            }
        }
        return cards.ToArray();
    }


    public static List<CardData> CreateFreshCopiesOf(List<CardData> deck)
    {
        List<CardData> fresh = new List<CardData>();
        foreach (CardData card in deck)
        {
            fresh.Add(card.CloneCardType());
        }
        return fresh;
    }

    public static List<CardData> ChooseNWithoutReplacement(List<CardData> pool, int n)
    {
        if(pool.Count < n)
        {
            Debug.LogError("Too few options! Trying to choose " + n + " options from a pool of size " + pool.Count);
        }
        List<CardData> chosen = new List<CardData>();
        List<int> validOption = new List<int>();
        for(int i = 0; i < pool.Count; ++i)
        {
            validOption.Add(i);
        }
        for(int i = 0; i < n; ++i)
        {
            int randIndex = Random.Range(0, validOption.Count);
            chosen.Add(pool[validOption[randIndex]].CloneCardType());
            validOption.RemoveAt(randIndex);
        }
        return chosen;
    }

    //Returns -1 if card is not found
    public static int FindCard(List<CardManager> mans, CardData data)
    {
        for(int i = 0; i < mans.Count; ++i)
        {
            if(!mans[i].IsEmpty() && mans[i].GetCardData().GetId() == data.GetId())
            {
                return i;
            }
        }
        return -1;
    }

    public static int FindCard(List<CardData> cards, CardData key)
    {
        for(int i = 0; i < cards.Count; ++i)
        {
            if(cards[i].GetId() == key.GetId())
            {
                return i;
            }
        }
        return -1;
    }

}
