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

    public Image Overlay;
    public Sprite DestroyIcon;
    public Sprite DiscardIcon;

    private List<CardData> playedCardsThisTurn = new List<CardData>();
    private List<CardData> cardsReturnedSoFar = new List<CardData>();

    private Stack<KeyValuePair<CardData, StackUsage>> playedCards = new Stack<KeyValuePair<CardData, StackUsage>>();
    private CardManager displayedCardData;
    private CardUIUpdater displayedCard;
    private EndTurnUI endTurn;
    private float currTime = 0;
    private float timePerCard = .5f;


    void Awake()
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
            if(currTime >= timePerCard 
                && !inAnimation)
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

    public void Push(CardData justPlayed, StackUsage usage)
    {
        endTurn.PauseAutoEndTurn();
        UpdateCounts(justPlayed);
        currTime = 0;
        playedCards.Push(MakePair(justPlayed, usage));
        UpdateUI();
    }

    private KeyValuePair<CardData, StackUsage> MakePair(CardData card, StackUsage usage)
    {
        return new KeyValuePair<CardData, StackUsage>(card, usage);
    }

    public void Pop()
    {
        KeyValuePair<CardData, StackUsage> cardAndUsage = playedCards.Pop();
        CardData top = cardAndUsage.Key;
        if (duplicate > 0 && !top.duplicated) //&& top.GetType().Equals(UICardData.CardType.SPELL))
        {
            Debug.Log("Duplicate active");
            DeckManager deck = DeckManager.Get();
            CardData copy = top.CloneCardType();//deck.Clone();
            top.fragile = true;
            top.duplicated = true;
            Push(copy, cardAndUsage.Value);
            duplicate--;
        }
        top.duplicated = false;

        ConsumeCardEffect(cardAndUsage);

        //Card Effects may require updating the hand such as Dual Weild
        GameObject.Find("Hand").GetComponent<HandManager>().UpdateAllCardsInHand();
        UpdateUI();
        inAnimation = false;
        endTurn.ResumeAutoEndTurn();    
    }

    private void ConsumeCardEffect(KeyValuePair<CardData, StackUsage> cardAndUsage)
    {
        StackUsage usage = cardAndUsage.Value;
        if(usage == StackUsage.PLAY)
        {
            PlayCard(cardAndUsage.Key);
            AddToDiscard(cardAndUsage.Key);
        }
        else if(usage == StackUsage.DISCARD)
        {
            AddToDiscard(cardAndUsage.Key);
        } else if(usage == StackUsage.DESTROY)
        {
           
            //Doing nothing removes it from the game
        } else if(usage == StackUsage.PLAY_AND_RETURN_TO_HAND)
        {
            PlayCard(cardAndUsage.Key);
            AddToHand(cardAndUsage.Key);
        }
    }

    private void PlayCard(CardData top)
    {
        if (top.getTarget().Equals(Target.CARD))
        {
            CardData[] targetCard = { top.selectedTarget.GetComponent<CardManager>().GetCardData() };
            top.Action(targetCard);
        }
        else if (top.getTarget().Equals(Target.ENEMY))
        {
            EnemyManager[] targetEnemy = { top.selectedTarget.GetComponent<EnemyManager>() };
            top.Action(targetEnemy);
        }
        else if (top.getTarget().Equals(Target.BOARD) || top.getTarget().Equals(Target.ALL_ENEMIES))
        {
            EnemyManager[] allEnemies = top.selectedTarget.GetComponentsInChildren<EnemyManager>();
            top.Action(allEnemies);
        }
    }

    private void AddToDiscard(CardData card)
    {
        if (!card.fragile)
        {
            DeckManager.Get().AddToDiscard(card);
        }
    }

    private void AddToHand(CardData card)
    {
        DeckManager.Get().addCardToHand(card);
    }

    private EnemyManager[] GetEnemiesToIndicateAsTargets()
    {
        if(playedCards.Count == 0
            || playedCards.Peek().Value != StackUsage.PLAY)
        {
            EnemyManager[] empty = { };
            return empty;
        }

        CardData top = playedCards.Peek().Key;

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
            CardData top = playedCards.Peek().Key;
            displayedCardData.Init(top);
            displayedCard.UpdateUI(top.GetUICardData());
        }
        UpdateOverlay();
        UpdateEnemyTargetIndicators();
    }

    private void UpdateOverlay()
    {
        CanvasGroup overlayCG = Overlay.GetComponent<CanvasGroup>();

        if (playedCards.Count == 0)
        {
            CanvasGroupManip.Disable(overlayCG);
        }
        else
        {
            StackUsage usage = playedCards.Peek().Value;
            if (usage == StackUsage.PLAY)
            {
                CanvasGroupManip.Disable(overlayCG);
            }
            else if (usage == StackUsage.DISCARD)
            {
                Overlay.sprite = DiscardIcon;
                CanvasGroupManip.Enable(overlayCG);
            }
            else if (usage == StackUsage.DESTROY)
            {
                Overlay.sprite = DestroyIcon;
                CanvasGroupManip.Enable(overlayCG);
            } else if(usage == StackUsage.PLAY_AND_RETURN_TO_HAND)
            {
                CanvasGroupManip.Disable(overlayCG);
            }
        }
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
