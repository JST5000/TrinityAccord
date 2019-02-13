using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Target GetTargets()
    {
        return target;
    }

    public void Action(EnemyManager[] enemys)
    {
        cardData.Action(enemys);
    }
    public void Action(CardData[] cards)
    {
        cardData.Action(cards);
    }
    public void Action(CardData[] cards, EnemyManager[] enemys)
    {
        cardData.Action(cards, enemys);
    }

    //Exposing data to UI
    public UICardData getUICardData()
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
