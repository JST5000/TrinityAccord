using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICardData
{
    public string cardName;
    public int cost;
    public string effectText;
    public enum CardType { SPELL, ATTACK };
    public CardType cardType;

    public UICardData(string cardName, int cost, string effectText, CardType type)
    {
        this.cardName = cardName;
        this.cost = cost;
        this.effectText = effectText;
        this.cardType = type;
    }

}
