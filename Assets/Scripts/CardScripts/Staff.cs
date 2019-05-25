using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : CardData
{
    CardData[] options = { new Staff1(), new Staff2(), new Staff3() };

    EnemyManager[] tempTarget;
    public Staff()
    {
        cardData = new UICardData("Staff", cost: 1, "Deal 2 damage, draw 1 card deal 1 damage, or draw 2 cards", UICardData.CardType.ATTACK);
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {

        GameObject instance = GameObject.Instantiate(Resources.Load<GameObject>("Choose3Menu"), GameObject.Find("Canvas").transform, false);
        Choose3Manager choose3 = instance.GetComponent<Choose3Manager>();
        DeckManager deck = GameObject.Find("Deck").GetComponent<DeckManager>();

        //Forces shuffle at start of effect instead of midway
        choose3.Init(options);

        //VERY IMPORTANT! 
        //This tells the choose 3 to send the decision to this card by caling the Action(CardData[] cards) function!
        choose3.SendDecisionTo(this);
        //Allows the menu to be hidden so the players can look at their cards when deciding.
        choose3.AllowHide();
        tempTarget = enemys;

    }

    //Will be called by the Choose3Manager when a choice has been made
    public override void Action(CardData[] cards)
    {
        if (cards[0].Equals(options[0]))
        {
            tempTarget[0].Damage(2 + sharpened);
        }
        else if (cards[0].Equals(options[1]))
        {
            tempTarget[0].Damage(1 + sharpened);
            draw();
        }else
        {
            draw();
            draw();
        }
    }

    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {

    }

    public override int SecondAction(CardManager card)
    {
        throw new System.NotImplementedException();
    }
    public override void sharpen()
    {
        sharpened++;
        for (int i = 0; i < sharpened; i++)
        {
            options[0].sharpen();
            options[1].sharpen();
        }
        cardData = new UICardData("Staff", cost: 1, "Deal " + (2 + sharpened) + " damage, draw 1 card deal " + (1 + sharpened) + " damage, or draw 2 cards", UICardData.CardType.ATTACK);
    }
}
