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
    private float timePerCard = 1f;
    public int cardsPlayed = 0;
    public int attacksPlayed = 0;
    public int duplicate = 0;

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

    public void Push(CardData justPlayed)
    {
        UpdateCounts(justPlayed);
        currTime = 0;
        playedCards.Push(justPlayed);
        UpdateUI();
    }

    public CardData Pop()
    {
        CardData top = playedCards.Pop();
        if (duplicate > 0&&!top.duplicated) //&& top.GetType().Equals(UICardData.CardType.SPELL))
        {
            Debug.Log("Duplicate active");
            DeckManager deck = GameObject.Find("Deck").GetComponent<DeckManager>();
            CardData copy = deck.Clone(top);
            top.fragile = true;
            top.duplicated = true;
            Push(copy);
            duplicate--;
        }
        top.duplicated = false;
        if (top.getTarget().Equals(Target.CARD))
        {
            CardData[] targetCard = { top.selectedTarget.GetComponent<CardManager>().GetCardData() };
            top.Action(targetCard);
        }else if (top.getTarget().Equals(Target.ENEMY))
        {
            EnemyManager[] targetEnemy = { top.selectedTarget.GetComponent<EnemyManager>() };
            top.Action(targetEnemy);
        }else if(top.getTarget().Equals(Target.BOARD)|| top.getTarget().Equals(Target.ALL_ENEMIES))
        {
            EnemyManager[] allEnemies = top.selectedTarget.GetComponentsInChildren<EnemyManager>();
            top.Action(allEnemies);
        }
        if (!top.fragile)
        {
            GameObject.Find("Deck").GetComponent<DeckManager>().AddToDiscard(top);
        }
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
    private void UpdateCounts(CardData card)
    {
        cardsPlayed++;
        if (card.GetUICardData().cardType.Equals(UICardData.CardType.ATTACK))
        {
            attacksPlayed++;
        }
    }
    public void ResetCounts()
    {
        cardsPlayed = 0;
        attacksPlayed = 0;
        duplicate = 0;
    }
}
