using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

public abstract class CardData
{
    protected UICardData cardData = new UICardData("Uninitialized", cost: 4, "Uninitialized", UICardData.CardType.ATTACK);

    //Target are for determining which user input is required. Ex. Tell the card which enemy is targeted.
    public  Target target;
    public GameObject selectedTarget;
    public string cardName;
    public int cost;
    public bool fragile = false;
    public bool duplicated = false;
    public abstract void Action(EnemyManager[] enemys);
    public abstract void Action(CardData[] cards);
    public abstract void Action(CardData[] cards, EnemyManager[] enemys);
    public abstract int SecondAction(CardManager card);

    public CardData Clone()
    {
        Type type = this.GetType();
        CardData copy = (CardData)Activator.CreateInstance(type);
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

    public UICardData GetUICardData()
    {
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
        while (!enemies[UnityEngine.Random.Range(0, enemies.Length)].Damage(amount))
        {
            if (!encounterActive())
            {
                return;
            }
        }
    }
    protected CardData draw()
    {
        DeckManager deck = GameObject.Find("Deck").GetComponent<DeckManager>();
        return deck.DrawCard();
    }
    protected CardData grabTop()
    {
        DeckManager deck = GameObject.Find("Deck").GetComponent<DeckManager>();
        return deck.grabTop();
    }
    protected bool encounterActive()
    {
        if (GameObject.Find("Board").GetComponent<EncounterManager>().enemyCount == 0)
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
