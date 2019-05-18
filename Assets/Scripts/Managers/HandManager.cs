﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public bool disableHand = false;


    public void UpdateAllCardsInHand()
    {
        UpdateAllCardsInHand(false);
    }

    public void UpdateAllCardsInHand(bool allOn)
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        int availableEnergy = player.GetEnergy();
        CardManager[] cardsInHand = GetComponentsInChildren<CardManager>();
        foreach (CardManager man in cardsInHand)
        {
            if (!man.IsEmpty())
            {
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

    public void EnableAllCardsInHand()
    {
        UpdateAllCardsInHand(true);
    }

    public bool HasPlayable()
    {
        CardManager[] cardsInHand = GetComponentsInChildren<CardManager>();
        foreach (CardManager man in cardsInHand)
        {
            if (!man.IsEmpty() && man.IsPlayable())
            {
                return true;
            }
        }
        return false;
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
