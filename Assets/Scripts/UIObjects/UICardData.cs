using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICardData
{
    public enum CardType { SPELL, ATTACK, QUEST };

    public string cardName { get; set; }
    public int cost { get; set; }
    public string effectText { get; set; }
    public CardType cardType { get; set; }
    public Sprite cardArt { get; set; }
    public bool displayOnlyCardArt { get; set; }

    public UICardData(string cardName, int cost, string effectText, CardType type)
    {
        this.cardName = cardName;
        this.cost = cost;
        this.effectText = effectText;
        this.cardType = type;
    }

    public UICardData(string cardName, int cost, string effectText, CardType type, string cardArtFileName) 
        : this(cardName, cost, effectText, type)
    {
        this.cardArt = Resources.Load<Sprite>("Card_Art\\" + cardArtFileName);
        if(cardArt == null)
        {
            Debug.Log("Unable to find the card art for " + cardArt);
        }
        //Default to full screen art with no text or show text
        this.displayOnlyCardArt = true;
    }

}
