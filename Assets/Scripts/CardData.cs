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
    public abstract Target Target();
    public abstract string CardName();
    public abstract int Cost();
    public abstract void Action(EnemyManager[] enemys);
    public abstract void Action(CardData[] cards);
    public abstract void Action(CardData[] cards, EnemyManager[] enemys);

    public UICardData GetUICardData()
    {
        return cardData;
    }
}
