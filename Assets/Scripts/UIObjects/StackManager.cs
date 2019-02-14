using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    private Stack<CardManager> playedCards = new Stack<CardManager>();
    private CardManager displayedCardData;
    private CardUIUpdater displayedCard;

    void Start()
    {
        displayedCardData = GetComponentInChildren<CardManager>();
        displayedCard = GetComponentInChildren<CardUIUpdater>();
    }

    public void Push(CardManager justPlayed)
    {
        playedCards.Push(justPlayed);
        UpdateUI();
    }

    public CardManager Pop()
    {
        CardManager top = playedCards.Pop();
        UpdateUI();
        return top;
    }

    private void UpdateUI()
    {
        if(playedCards.Count == 0)
        {
            displayedCardData.SetEmpty();
        } else
        {
            displayedCardData.Init(playedCards.Peek().GetCardData());
            displayedCard.UpdateUI(playedCards.Peek().GetUICardData());
        }
    }
}
