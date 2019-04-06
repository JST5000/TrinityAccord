using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
enum CardLocation { Hand, Stack}
public class CardManager : MonoBehaviour
{

    Target target;
    //string cardName;
    int cost;
    CardData cardData;
    public bool empty;
    public void Init(CardData cardData)
    {
        this.target = cardData.getTarget();
        //this.cardName = cardData.getName();
        this.cost = cardData.getCost();
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
    public void discard()
    {
        GameObject.Find("Deck").GetComponent<DeckManager>().AddToDiscard(cardData);
        SetEmpty();

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

    public void Action(CardData[] cards, ref EnemyManager[] enemys)
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

    public bool IsPlayable()
    {
        return cardData.IsPlayable();
    }
}
