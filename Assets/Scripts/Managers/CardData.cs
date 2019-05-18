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

    protected UICardData cardData = new UICardData("Uninitialized", cost: 4, "Uninitialized", UICardData.CardType.ATTACK);

    //Target are for determining which user input is required. Ex. Tell the card which enemy is targeted.
    public  Target target;
    public GameObject selectedTarget;
    public string cardName;
    public int cost;

    public int sharpened = 0;
    public int changedCost=-1;

    public bool fragile = false;
    public bool duplicated = false;

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
    }

    protected CardData(UICardData baseline, Target target)
    {
        lock (IdLock) {
            id = nextId;
            ++nextId;
        }
        this.cardData = baseline;
        this.cost = baseline.cost;
        this.target = target;

    }
    public virtual void sharpen()
    {

    }
    public void setCost(int cost)
    {
        this.cost = cost;
        cardData.cost = cost;
    }
    public void setChangedCost(int cost)
    {
        changedCost = cost;
    }

    public int GetId()
    {
        return id;
    }

    public CardData Clone()
    {
        Type type = this.GetType();
        CardData copy = (CardData)Activator.CreateInstance(type);
        copy.id = this.id;
        return copy;
    }

    //Does basic check of mana cost/availability. Extra requirements must be implemented separately.
    public bool IsPlayable()
    {
        bool playerHasEnoughEnergy = GameObject.Find("Player").GetComponent<Player>().GetEnergy() >= cost;
        return playerHasEnoughEnergy && IsPlayableAdditionalRequirements();
    }

    //Default implementation. For cards that need further checks, override this function.
    public virtual bool IsPlayableAdditionalRequirements()
    {
        return true;
    }
    public virtual void onDiscard()
    {

    }

    public UICardData GetUICardData()
    {
        if (changedCost != -1)
        {
            cardData.cost = changedCost;
            cost = changedCost;
        }
        return cardData;
    }
/*    public CardData Clone(CardData card)
    {
        DeckManager deck = GameObject.Find("Deck").GetComponent<DeckManager>();
        return deck.Clone(card);
    } */
    public string getName()
    {
        return cardData.cardName;
    }
    public Target getTarget()
    {
        return target;
    }
    public int getCost()
    {
        return cost;
    }
    public UICardData.CardType getType()
    {
        return cardData.cardType;
    }
    protected void damageRandom(int amount)
    {
        EnemyManager[] enemies = GameObject.Find("Board").GetComponent<EncounterManager>().allEnemyManagers;
        List<int> validEnemies = new List<int>();
        for(int i = 0; i < enemies.Length; ++i)
        {
            if(!enemies[i].IsEmpty())
            {
                validEnemies.Add(i);
            }
        }
        //Nothing to damage
        if(validEnemies.Count == 0)
        {
            return;
        }
        int randomIndex = UnityEngine.Random.Range(0, validEnemies.Count);
        for (int i = 0; i < 100 && !enemies[validEnemies[randomIndex]].Damage(amount); ++i) //Used instead of while to avoid infinite loop on error
        {
            if (!encounterActive())
            {
                return;
            }
            randomIndex = UnityEngine.Random.Range(0, enemies.Length);
        } 
    }
    protected CardData draw()
    {
        DeckManager deck = GameObject.Find("Deck").GetComponent<DeckManager>();
        return deck.DrawCard();
    }
    protected void drawFromDiscard()
    {
        DeckManager deck = GameObject.Find("Deck").GetComponent<DeckManager>();
        CardData toAdd= deck.grabDiscard();
        if (toAdd != null) {
            deck.addCardToHand(toAdd);
        }

    }
    protected CardData grabTop()
    {
        DeckManager deck = GameObject.Find("Deck").GetComponent<DeckManager>();
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

        return GameObject.Find("Deck").GetComponent<DeckManager>().getNumberOfCardsInHand();
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
        foreach (CardManager cardManager in GameObject.Find("Deck").GetComponent<DeckManager>().hand)
        {
            if (cardManager.GetCardData().Equals(this))
            {
                return cardManager;
            }
        }
        return null;
    }
    protected CardManager[] getHand()
    {
        return GameObject.Find("Deck").GetComponent<DeckManager>().hand;
    }
    protected DeckManager getDeckManager()
    {
        return GameObject.Find("Deck").GetComponent<DeckManager>();
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
            card.selectedTarget=GameObject.Find("Deck").GetComponent<DeckManager>().getRandomCardTarget();
        }
        else
        {
            card.selectedTarget = GameObject.Find("Board");
        }
        GameObject.Find("StackHolder").GetComponent<StackManager>().Push(card);
    }
    protected void playCardRandomTarget(CardData card)
    {
        if (card.target.Equals(Target.ENEMY))
        {
            GameObject[] enemies = GameObject.Find("Board").GetComponent<EncounterManager>().enemyGameObjects;

            List<int> validEnemies = new List<int>();
            for (int i = 0; i < enemies.Length; ++i)
            {
                if (!enemies[i].GetComponent<EnemyManager>().IsEmpty())
                {
                    validEnemies.Add(i);
                }
            }
            //Nothing to damage
            if (validEnemies.Count == 0)
            {

                return;
            }
            int randomIndex = UnityEngine.Random.Range(0, validEnemies.Count);
            card.selectedTarget = (GameObject)enemies[validEnemies[randomIndex]];
        }
        else if (card.target.Equals(Target.CARD))
        {
            card.selectedTarget = GameObject.Find("Deck").GetComponent<DeckManager>().getRandomCardTarget();
        }
        else
        {
            card.selectedTarget = GameObject.Find("Board");
        }
        GameObject.Find("StackHolder").GetComponent<StackManager>().Push(card);
    }
    protected void addCardToDiscard(CardData card)
    {
        GameObject.Find("Deck").GetComponent<DeckManager>().AddToDiscard(card);
    }
    /*
    protected Choose3Manager GetChoose3Manager()
    {
        return GameObject.Find
    }
    */
}
