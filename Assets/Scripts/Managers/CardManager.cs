using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
enum CardLocation { Hand, Stack}
public class CardManager : MonoBehaviour
{

    CardData cardData;

    public bool empty;

    public bool IsInHand = false;

    public bool AlwaysDisplayText;

    private bool Preserve;

    private bool prevSettingForCardArt;

    public void Init(CardData cardData)
    {
        if (cardData.CannotBePlayed && IsInHand)
        {
            return;
        }
        else
        {
            this.cardData = cardData;
            if (AlwaysDisplayText)
            {
                cardData.GetUICardData().displayOnlyCardArt = false;
            }
            else
            {
                cardData.GetUICardData().displayOnlyCardArt = true;
            }
            this.empty = false;
            this.Preserve = false;
        }
    }

    public void SetEmpty()
    {
        empty = true;
        this.Preserve = false;
    }

    public bool IsEmpty()
    {
        return empty;
    }

    public Target GetTargets()
    {
        return cardData.getTarget();
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

    public void SetCardArtOpacity(bool displayOnlyCardArt)
    {
        if(AlwaysDisplayText)
        {
            cardData.GetUICardData().displayOnlyCardArt = false;

        } else {
            cardData.GetUICardData().displayOnlyCardArt = displayOnlyCardArt;
        }
    }

    public void PreserveCard()
    {
        Preserve = true;
    }

    public void DisablePreserved()
    {
        Preserve = false;
    }

    public bool IsPreserved()
    {
        return Preserve;
    }

    public bool IsPlayable()
    {
        return cardData.IsPlayable();
    }
}
