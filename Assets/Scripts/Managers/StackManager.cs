using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StackManager : MonoBehaviour
{
    public Text label;

    public int cardsPlayed = 0;
    public int attacksPlayed = 0;
    public int duplicate = 0;
    public bool inAnimation = false;

    private List<CardData> playedCardsThisTurn = new List<CardData>();
    private List<CardData> cardsReturnedSoFar = new List<CardData>();

    private Stack<CardData> playedCards = new Stack<CardData>();
    private CardManager displayedCardData;
    private CardUIUpdater displayedCard;
    private EndTurnUI endTurn;
    private float currTime = 0;
    private float timePerCard = .5f;


    void Start()
    {
        displayedCardData = GetComponentInChildren<CardManager>();
        displayedCard = GetComponentInChildren<CardUIUpdater>();
        endTurn = GameObject.Find("EndTurnButton").GetComponent<EndTurnUI>();
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
            if(currTime >= timePerCard&&!inAnimation)
            {
                inAnimation = true;
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

    public bool IsDisplayEmpty()
    {
        return displayedCardData.IsEmpty();
    }

    public void Push(CardData justPlayed)
    {
        endTurn.PauseAutoEndTurn();
        UpdateCounts(justPlayed);
        currTime = 0;
        playedCards.Push(justPlayed);
        UpdateUI();
    }

    public void Pop()
    {
        CardData top = playedCards.Pop();
        if (duplicate > 0 && !top.duplicated) //&& top.GetType().Equals(UICardData.CardType.SPELL))
        {
            Debug.Log("Duplicate active");
            DeckManager deck = GameObject.Find("Deck").GetComponent<DeckManager>();
            CardData copy = top.Clone();//deck.Clone();
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

        //Card Effects may require updating the hand such as Dual Weild
        GameObject.Find("Hand").GetComponent<HandManager>().UpdateAllCardsInHand();
        UpdateUI();
        inAnimation = false;
        endTurn.ResumeAutoEndTurn();    
    }

    private EnemyManager[] GetEnemiesToIndicateAsTargets()
    {
        if(playedCards.Count == 0)
        {
            EnemyManager[] empty = { };
            return empty;
        }
        CardData top = playedCards.Peek();

        if (top.getTarget().Equals(Target.CARD) || top.getTarget().Equals(Target.BOARD))
        {
            EnemyManager[] empty = { };
            return empty;
        }
        else if (top.getTarget().Equals(Target.ENEMY))
        {
            EnemyManager[] targetEnemy = { top.selectedTarget.GetComponent<EnemyManager>() };
            return targetEnemy;
        }
        else if (top.getTarget().Equals(Target.ALL_ENEMIES))
        {
            EnemyManager[] allEnemies = top.selectedTarget.GetComponentsInChildren<EnemyManager>();
            return allEnemies;
        }
        throw new KeyNotFoundException("Tried to get card targets, but did not recognize the target recieved. " + top.getTarget());
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
        UpdateEnemyTargetIndicators();
    }

    private void UpdateEnemyTargetIndicators()
    {
        EnemyManager[] targets = GetEnemiesToIndicateAsTargets();
        GameObject.Find("Board").GetComponent<EncounterManager>().SetTargetInidcators(targets);
    }

    private void UpdateCounts(CardData card)
    {
        cardsPlayed++;
        //Avoids multiple uses of a card causing nulls later
        if (!playedCardsThisTurn.Contains(card))
        {
            playedCardsThisTurn.Add(card);
        }
        if (card.GetUICardData().cardType.Equals(UICardData.CardType.ATTACK))
        {
            attacksPlayed++;
        }
    }
    public void ResetCounts()
    {
        cardsPlayed = 0;
        playedCardsThisTurn.Clear();
        cardsReturnedSoFar.Clear();
        attacksPlayed = 0;
        duplicate = 0;
    }

    //Returns one of the cards played this turn.
    //Returns null if no cards were played this turn
    public CardData GetRandomCardPlayedThisTurn()
    {
        if (playedCardsThisTurn.Count <= cardsReturnedSoFar.Count)
        {
            return null;
        } else {
            List<CardData> uniqueOptions = new List<CardData>();
            foreach(CardData card in playedCardsThisTurn)
            {
                if(!cardsReturnedSoFar.Contains(card))
                {
                    uniqueOptions.Add(card);
                }
            }
            CardData selected = CardDataUtil.ChooseNWithoutReplacement(uniqueOptions, 1)[0];
            cardsReturnedSoFar.Add(selected);
            return selected;
        }
    }
}
