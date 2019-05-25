using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICardData
{
    public enum CardType { SPELL, ATTACK };

    public string cardName;
    public int cost;
    public string effectText;
    public CardType cardType;
    public Sprite cardArt;
    public bool maximize;

    public UICardData(string cardName, int cost, string effectText, CardType type)
    {
        this.cardName = cardName;
        this.cost = cost;
        this.effectText = effectText;
        this.cardType = type;
    }

    public UICardData(string cardName, int cost, string effectText, CardType type, Sprite cardArt, bool maximize) 
        : this(cardName, cost, effectText, type)
    {
        this.cardArt = cardArt;
        this.maximize = maximize;
    }

}
