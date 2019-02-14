using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum CardLocation { Hand, Stack}
public class CardManager : MonoBehaviour
{

    Target target;
    string cardName;
    int cost;
    CardData cardData;
    public bool empty;
    public void Init(CardData cardData)
    {
        this.target = cardData.Target();
        this.cardName = cardData.CardName();
        this.cost = cardData.Cost();
        this.cardData = cardData;
        this.empty = false;
    }
    public void SetEmpty()
    {
        empty = true;
    }

    public bool IsEmpty()
    {
        return empty;
    }

    public Target GetTargets()
    {
        return target;
    }

    public void Action(ref EnemyManager[] enemys)
    {
        cardData.Action(enemys);
    }
    public void Action(ref CardData[] cards)
    {
        cardData.Action(cards);
    }
    public void Action(ref CardData[] cards, ref EnemyManager[] enemys)
    {
        cardData.Action(cards, enemys);
    }

    //Exposing CardData to clone the one card to another
    public CardData GetCardData()
    {
        return cardData;
    }

    //Exposing data to UI
    public UICardData GetUICardData()
    {
        if (cardData != null)
        {
            return cardData.GetUICardData();
        } else
        {
            return null;
        }
    }
}
