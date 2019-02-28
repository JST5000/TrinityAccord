using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//public enum Target {Enemies,board}
//REPLACED with Target enum
//DELETE THESE COMMENTS WHEN YOU READ THIS AJ
public abstract class CardData
{
    protected UICardData cardData = new UICardData("Uninitialized", cost: 4, "Uninitialized", UICardData.CardType.ATTACK);

    //Target are for determining which user input is required. Ex. Tell the card which enemy is targeted.
    public  Target target;
    public string cardName;
    public int cost;
    public abstract void Action(EnemyManager[] enemys);
    public abstract void Action(CardData[] cards);
    public abstract void Action(CardData[] cards, EnemyManager[] enemys);

    //Does basic check of mana cost/availability. Extra requirements must be implemented separately.
    public bool IsPlayable()
    {
        bool playerHasEnoughEnergy = GameObject.Find("Player").GetComponent<Player>().GetEnergy() >= cost;
        return playerHasEnoughEnergy && IsPlayableAdditionalRequirements();
    }

    //Default implementation. For cards that need further checks, override this function.
    public bool IsPlayableAdditionalRequirements()
    {
        return true;
    }

    public UICardData GetUICardData()
    {
        return cardData;
    }
    public string getName()
    {
        return cardName;
    }
    public Target getTarget()
    {
        return target;
    }
    public int getCost()
    {
        return cost;
    }
    protected CardData draw()
    {
        DeckManager deck = GameObject.Find("Deck").GetComponent<DeckManager>();
        return deck.DrawCard();
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
}
