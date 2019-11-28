using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public bool disableHand = false;

    CardManager[] cardsInHand;

    private void LoadCardsInHand()
    {
        if (cardsInHand == null)
        {
            cardsInHand = GetComponentsInChildren<CardManager>();
        }
    }

    public static HandManager Get()
    {
        return GameObject.Find("Hand")?.GetComponent<HandManager>();
    }

    public void UpdateAllCardsInHand()
    {
        UpdateAllCardsInHand(false);
    }

    public void UpdateAllCardsInHand(bool allOn)
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        int availableEnergy = player.GetEnergy();
        LoadCardsInHand();
        foreach (CardManager man in cardsInHand)
        {
            if (!man.IsEmpty())
            {
                man.GetCardData().UpdateUICardData();

                CardUIUpdater manUI = man.transform.GetComponent<CardUIUpdater>();
                //If we are selecting cards in hand, their playability does not matter (Likely a discard effect)
                if (!allOn && (disableHand || (!man.IsPlayable() && !(UIManager.currentMode == GameMode.PickCardInHand))))
                {
                    manUI.DisableCard();
                }
                else
                {
                    manUI.EnableCard();
                }
            }
        }
    }

    public void RemoveCardFromHand(int cardId)
    {
        LoadCardsInHand();
        foreach (CardManager man in cardsInHand)
        {
            if(!man.IsEmpty()
                && man.GetCardData().GetId() == cardId)
            {
                man.SetEmpty();
                
                break;
            } else
            {
                Debug.Log(man?.GetCardData()?.GetId());
            }
        }
    }

    public void EnableAllCardsInHand()
    {
        UpdateAllCardsInHand(true);
    }

    public bool HasPlayable()
    {
        LoadCardsInHand();
        foreach (CardManager man in cardsInHand)
        {
            if (!man.IsEmpty() && man.IsPlayable())
            {
                return true;
            }
        }
        return false;
    }

    public CardManager GetManagerWithCard(CardData card)
    {
        foreach (CardManager man in cardsInHand)
        {
            if(!man.IsEmpty() && man.GetCardData().Equals(card))
            {
                return man;
            }
        }
        return null;
    }

    public void DisableHandInteractions()
    {
        disableHand = true;
        UpdateAllCardsInHand();
    }

    public void EnableHandInteraction()
    {
        disableHand = false;
        UpdateAllCardsInHand();
    }
}
