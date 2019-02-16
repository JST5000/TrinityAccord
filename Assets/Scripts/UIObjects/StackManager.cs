using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    private Stack<CardManager> playedCards = new Stack<CardManager>();
    private CardManager displayedCardData;
    private CardUIUpdater displayedCard;
    private float currTime = 0;
    private float timePerCard = .5f;

    void Start()
    {
        displayedCardData = GetComponentInChildren<CardManager>();
        displayedCard = GetComponentInChildren<CardUIUpdater>();
    }

    private void Update()
    {
        if(playedCards.Count != 0)
        {
            currTime += Time.deltaTime;
            if(currTime >= timePerCard)
            {
                currTime = 0;
                Pop();
            }
        }
    }

    public bool IsEmpty()
    {
        return playedCards.Count == 0;
    }

    public void Push(CardManager justPlayed)
    {
        currTime = 0;
        playedCards.Push(justPlayed);
        UpdateUI();
    }

    public CardManager Pop()
    {
        CardManager top = playedCards.Pop();
        GameObject.Find("Deck").GetComponent<DeckManager>().AddToDiscard(top.GetCardData());
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
