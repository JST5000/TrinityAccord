using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    int target;
    string cardName;
    int cost;
    CardData cardData;
    public void Init(CardData cardData)
    {
        this.target = cardData.target;
        this.cardName = cardData.cardName;
        this.cost = cardData.cost;
        this.cardData = cardData;
    }
    public int GetTargets()
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
    public void Action(CardData[] cards,EnemyManager[] enemys)
    {
        cardData.Action(cards,enemys);
    }
}
