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
    public GameObject selectedTarget;
    public string cardName;

    protected int sharpenDamage = 0;

    public bool fragile = false;
    public bool duplicated = false;

    public bool CannotBePlayed = false;

    public bool pauseGameplay = false;

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

    public UICardData GetUICardData()
    {
        return uiCardData;
    }

    protected void UpdateUICardData(bool doNotUpdateCost = true)
    {
        UICardData updated = CreateUICardData();
        if(uiCardData != null && doNotUpdateCost)
        {
            updated.cost = uiCardData.cost;
        }
        uiCardData = updated;
    }

    protected abstract UICardData CreateUICardData();

    public virtual void sharpen()
    {
        sharpenDamage++;
        UpdateUICardData();
    }

    public virtual int GetBonusDamage()
    {
        return sharpenDamage;
    }

    public virtual void OnSelectedInHand()
    {

    }

    public void setCost(int cost)
    {
        uiCardData.cost = cost;
    }

    public int GetId()
    {
        return id;
    }

    public CardData CloneCardType()
    {
        Type type = this.GetType();
        CardData copy = (CardData)Activator.CreateInstance(type);
        copy.id = this.id;
        return copy;
    }

    //Does basic check of mana cost/availability. Extra requirements must be implemented separately.
    public bool IsPlayable()
    {
        bool playerHasEnoughEnergy = GameObject.Find("Player").GetComponent<Player>().GetEnergy() >= uiCardData.cost;
        return playerHasEnoughEnergy && IsPlayableAdditionalRequirements();
    }

    //Default implementation. For cards that need further checks, override this function.
    public virtual bool IsPlayableAdditionalRequirements()
    {
        return true;
    }
    public virtual void OnDiscard() { }
    public virtual void OnDraw() { }


    public string getName()
    {
        return uiCardData.cardName;
    }

    public Target getTarget()
    {
        return target;
    }

    public int getCost()
    { 
        return uiCardData.cost;
    }

    public UICardData.CardType getType()
    {
        return uiCardData.cardType;
    }

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

    protected void drawFromDiscard()
    {
        DeckManager deck = DeckManager.Get();
        CardData toAdd= deck.grabDiscard();
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
        return GameObject.Find("StackHolder").GetComponent<StackManager>().attacksPlayed;
    }

    protected int getNumberOfCardsPlayed()
    {
        return GameObject.Find("StackHolder").GetComponent<StackManager>().cardsPlayed;
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
            card.selectedTarget = this.selectedTarget;
        }
        else if (card.target.Equals(Target.CARD))
        {
            card.selectedTarget = DeckManager.Get().GetRandomValidCardManagerFromHand().gameObject;
        }
        else
        {
            card.selectedTarget = GameObject.Find("Board");
        }
        GameObject.Find("StackHolder").GetComponent<StackManager>().Push(card, StackUsage.PLAY);
    }

    public static void playCardRandomTarget(CardData card, StackUsage usage = StackUsage.PLAY)
    {
        if (card.target.Equals(Target.ENEMY))
        {
            card.selectedTarget = GameObject.Find("Board").GetComponent<EncounterManager>().GetRandomAliveEnemyManager().gameObject;
        }
        else if (card.target.Equals(Target.CARD))
        {
            card.selectedTarget = DeckManager.Get().GetRandomValidCardManagerFromHand().gameObject;
        }
        else
        {
            card.selectedTarget = GameObject.Find("Board");
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
}
