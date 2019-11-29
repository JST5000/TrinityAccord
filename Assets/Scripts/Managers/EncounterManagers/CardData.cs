using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

public abstract class CardData
{
    //For giving each card created a unique identifier
    protected static int nextId = 0;
    protected static object IdLock = new object();
    private int id;

    protected UICardData uiCardData = null;

    //Target are for determining which user input is required. Ex. Tell the card which enemy is targeted.
    public  Target target;
    public GameObject SelectedTarget { get; set; }

    protected int sharpenDamage = 0;

    public bool fragile = false;

    public bool CannotBePlayed = false;

    public bool pauseGameplay = false;

    /// <summary>
    /// Could be improved by putting in a lambda condition for cards to check if they fit (Instead of one for each condition we care about).
    /// </summary>
    public static int FixedCostForSpells { get; set; } = -1;
    public int PreviousCost = -1;

    /// <summary>
    /// Used for Quests to change into their completed forms. 
    /// This could be broken out when we refactor CardTypes into subclasses
    /// </summary>
    public virtual bool Transformed
    {
        get
        {
            return transformed;
        }
        set
        {
            transformed = value;
            UpdateUICardData(false);
        }
    }

    private bool transformed = false;

    public float animationTime;
    public abstract void Action(EnemyManager[] enemys);
    public abstract void Action(CardData[] cards);
    public abstract void Action(CardData[] cards, EnemyManager[] enemys);
    public abstract int SecondAction(CardManager card);

    //TODO - Remove this one and move everything into the CardData(UICardData, Target) constructor to guarentee correct initialization
    protected CardData()
    {
        lock (IdLock)
        {
            id = nextId;
            ++nextId;
        }
        UpdateUICardData();
    }

    protected CardData(UICardData baseline, Target target)
    {
        lock (IdLock) {
            id = nextId;
            ++nextId;
        }
        this.uiCardData = baseline;
        this.target = target;
        UpdateUICardData();
    }

    protected abstract UICardData CreateUICardData();

    public UICardData GetUICardData()
    {
        return uiCardData;
    }

    public void UpdateUICardData(bool doNotUpdateCost = true)
    {
        UICardData updated = CreateUICardData();
        if(uiCardData != null && doNotUpdateCost)
        {
            updated.cost = uiCardData.cost;
        }
        //Could do a general function here for what condition triggers the temporary changes
        if(uiCardData != null && uiCardData.cardType.Equals(UICardData.CardType.SPELL))
        {
            if (FixedCostForSpells >= 0 && PreviousCost < 0)
            {
                PreviousCost = updated.cost;
                updated.cost = FixedCostForSpells;
            }

            if(FixedCostForSpells < 0 && PreviousCost >= 0)
            {
                updated.cost = PreviousCost;
                PreviousCost = -1;
            }
        } 
        uiCardData = updated;
    }

    public virtual void Sharpen()
    {
        sharpenDamage++;
        UpdateUICardData();
    }

    public virtual int GetBonusDamage()
    {
        return sharpenDamage;
    }

    public void OnSelectedInHand()
    {
        if (target.Equals(Target.CARD))
        {
            HandManager.Get().EnableAllCardsInHand();
        }
    }

    public void SetCost(int cost)
    {
        uiCardData.cost = cost;
    }

    public int GetId()
    {
        return id;
    }

    public CardData CloneCard()
    {
        Type type = this.GetType();
        CardData copy = (CardData)Activator.CreateInstance(type);
        copy.id = this.id;
        return copy;
    }

    /// <summary>
    /// Does basic check of mana cost/availability. Extra requirements must be implemented separately.
    /// </summary>
    /// <returns></returns>
    public bool IsPlayable()
    {
        bool playerHasEnoughEnergy = GameObject.Find("Player").GetComponent<Player>().GetEnergy() >= uiCardData.cost;
        return playerHasEnoughEnergy;
    }

    public virtual void OnDiscard() { }
    public virtual void OnDraw() { }


    public string GetName()
    {
        return uiCardData.cardName;
    }

    public Target GetTarget()
    {
        return target;
    }

    public int GetCost()
    { 
        return uiCardData.cost;
    }

    public UICardData.CardType GetTypeOfCard()
    {
        return uiCardData.cardType;
    }

    /// <summary>
    /// R
    /// </summary>
    /// <returns></returns>
    protected EnemyManager GetRandomEnemy()
    {
        EncounterManager encounter = GameObject.Find("Board").GetComponent<EncounterManager>();
        EnemyManager targetEnemy = null;
        if (encounter.GetTargetedEnemy() != null)
        {
            targetEnemy = encounter.GetTargetedEnemy();
        }
        else
        {
            List<int> validEnemies = new List<int>();
            EnemyManager[] enemies = encounter.allEnemyManagers;
            for (int i = 0; i < enemies.Length; ++i)
            {
                if (!enemies[i].IsEmpty())
                {
                    validEnemies.Add(i);
                }
            }
            int randomIndex = UnityEngine.Random.Range(0, validEnemies.Count);
            if (validEnemies.Count != 0)
            {
                targetEnemy = enemies[validEnemies[randomIndex]];
            }
        }
        return targetEnemy;
    }

    protected void damageRandom(int amount)
    {
        EnemyManager targetEnemy = GetRandomEnemy();
        //Nothing to damage
        if (targetEnemy == null)
        {
            return;
        }
        DamageRandomEnemyHelper(targetEnemy, amount);

    }

    private void DamageRandomEnemyHelper(EnemyManager target, int amount)
    {
        for (int i = 0; i < 100 && !target.Damage(amount); ++i) //Used instead of while to avoid infinite loop on error
        {
            if (!encounterActive())
            {
                return;
            }
            //randomIndex = UnityEngine.Random.Range(0, enemies.Length);
        }
    }

    protected CardData draw()
    {
        DeckManager deck = DeckManager.Get();
        return deck.DrawCard();
    }

    protected void drawFromDiscard(int index = -1)
    {
        DeckManager deck = DeckManager.Get();
        CardData toAdd= deck.grabDiscard(index);
        if (toAdd != null) {
            deck.addCardToHand(toAdd);
        }

    }
    protected CardData grabTop()
    {
        DeckManager deck = DeckManager.Get();
        return deck.grabTop();
    }

    protected bool encounterActive()
    {
        if (GameObject.Find("Board").GetComponent<EncounterManager>().enemyCount <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void addEnergy(int amount)
    {
        GameObject.Find("Player").GetComponent<Player>().AddEnergy(amount);
    }

    protected void addEnergyNextTurn(int amount)
    {
        GameObject.Find("Player").GetComponent<Player>().addBonusEnergy(amount);
    }

    protected void selectCard(int amount)
    {
       UIManager.selectCardInHand(this,amount);
    }

    protected int checkNumberOfCardsInHand()
    {

        return DeckManager.Get().getNumberOfCardsInHand();
    }

    protected int getNumberOfAttacksPlayed()
    {
        return GameObject.Find("StackHolder").GetComponent<StackManager>().AttacksPlayedThisTurn;
    }

    protected int getNumberOfCardsPlayed()
    {
        return GameObject.Find("StackHolder").GetComponent<StackManager>().cardsPlayedThisTurn;
    }

    protected CardManager getMyCardManager()
    {
        foreach (CardManager cardManager in DeckManager.Get().hand)
        {
            if (cardManager.GetCardData() == this)
            {
                return cardManager;
            }
        }
        return null;
    }

    protected CardManager[] getHand()
    {
        return DeckManager.Get().hand;
    }

    protected DeckManager getDeckManager()
    {
        return DeckManager.Get();
    }

    protected EnemyManager[] getEnemyManagers()
    {
        return GameObject.Find("Board").GetComponent<EncounterManager>().allEnemyManagers;
    }

    protected void playCardSameTarget(CardData card)
    {
        if (card.target.Equals(Target.ENEMY))
        {
            card.SelectedTarget = this.SelectedTarget;
        }
        else if (card.target.Equals(Target.CARD))
        {
            card.SelectedTarget = DeckManager.Get().GetRandomValidCardManagerFromHand().gameObject;
        }
        else
        {
            card.SelectedTarget = GameObject.Find("Board");
        }
        GameObject.Find("StackHolder").GetComponent<StackManager>().Push(card, StackUsage.PLAY);
    }

    public static void playCardRandomTarget(CardData card, StackUsage usage = StackUsage.PLAY)
    {
        if (card.target.Equals(Target.ENEMY))
        {
            card.GetRandomAliveEnemyAsTarget();
        }
        else if (card.target.Equals(Target.CARD))
        {
            card.SelectedTarget = DeckManager.Get().GetRandomValidCardManagerFromHand().gameObject;
        }
        else
        {
            card.SelectedTarget = GameObject.Find("Board");
        }
        GameObject.Find("StackHolder").GetComponent<StackManager>().Push(card, usage);
    }

    protected void addCardToDiscard(CardData card)
    {
        DeckManager.Get().AddToDiscard(card);
    }

    protected CardData BurnTopCard()
    {
        CardData top = getDeckManager().grabTop();
        StackManager playStack = GameObject.Find("StackHolder").GetComponent<StackManager>();
        playStack.Push(top, StackUsage.DESTROY);
        return top;
    }

    protected void DiscardCardOnStack(CardData card)
    {
        StackManager playStack = GameObject.Find("StackHolder").GetComponent<StackManager>();
        playStack.Push(card, StackUsage.DISCARD);
    }

    protected void PauseExecutionUntilChoiceMade()
    {
        GameObject.Find("EndTurnButton").GetComponent<EndTurnUI>().PauseAutoEndTurn();
        StackManager.Get().PauseExecution();
    }

    protected void ResumeExecution()
    {
        GameObject.Find("EndTurnButton").GetComponent<EndTurnUI>().ResumeAutoEndTurn();
        StackManager.Get().ResumeExecution();
    }

    public virtual string GetBonusEffectDescription()
    {
        return "";
    }

    /// <summary>
    /// Sets the target to null if no enemies are alive
    /// </summary>
    public void GetRandomAliveEnemyAsTarget()
    {
        SelectedTarget = GameObject.Find("Board").GetComponent<EncounterManager>().GetRandomAliveEnemyManager()?.gameObject;
    }
}
