using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardDataUtil 
{
    public static List<CardData> CreateFreshCopiesOf(List<CardData> deck)
    {
        List<CardData> fresh = new List<CardData>();
        foreach (CardData card in deck)
        {
            fresh.Add(card.Clone());
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
            chosen.Add(pool[validOption[randIndex]].Clone());
            validOption.RemoveAt(randIndex);
        }
        return chosen;
    }
}
