using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StackManager : MonoBehaviour
{
    public Text label;

    private Stack<CardData> playedCards = new Stack<CardData>();
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
        if(!IsEmpty() && label.color.a != 0)
        {
            SetAlphaColor(label, 0);
        } else if(IsEmpty() && label.color.a == 0)
        {
            SetAlphaColor(label, 1);
        }

        if (playedCards.Count != 0)
        {
            currTime += Time.deltaTime;
            if(currTime >= timePerCard)
            {
                currTime = 0;
                Pop();
            }
        }
    }

    private void SetAlphaColor(Text label, float a)
    {
        var color = label.color;
        color.a = a;
        label.color = color;
    }

    public bool IsEmpty()
    {
        return playedCards.Count == 0;
    }

    public void Push(CardManager justPlayed)
    {
        currTime = 0;
        playedCards.Push(justPlayed.GetCardData());
        UpdateUI();
    }

    public CardData Pop()
    {
        CardData top = playedCards.Pop();
        GameObject.Find("Deck").GetComponent<DeckManager>().AddToDiscard(top);
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
            displayedCardData.Init(playedCards.Peek());
            displayedCard.UpdateUI(playedCards.Peek().GetUICardData());
        }
    }
}
