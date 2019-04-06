using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{

    public void UpdateAllCardsInHand()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        int availableEnergy = player.GetEnergy();
        CardManager[] cardsInHand = GetComponentsInChildren<CardManager>();
        foreach(CardManager man in cardsInHand)
        {
            if (!man.IsEmpty())
            {
                CardUIUpdater manUI = man.transform.GetComponent<CardUIUpdater>();
                if (availableEnergy < man.GetCardData().cost)
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
}
