using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : CardData
{
    CardData[] options = { new Staff1(), new Staff2(), new Staff3() };

    EnemyManager[] tempTarget;
    public Staff()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Staff", cost: 1, "Deal " + (2 + GetBonusDamage()) + " damage, draw 1 card deal " + (1 + GetBonusDamage()) + " damage, or draw 2 cards", UICardData.CardType.ATTACK);
    }

    public override void Action(EnemyManager[] enemys)
    {

        GameObject instance = GameObject.Instantiate(Resources.Load<GameObject>("Choose3Menu"), GameObject.Find("Canvas").transform, false);
        Choose3Manager choose3 = instance.GetComponent<Choose3Manager>();
        DeckManager deck = DeckManager.Get();

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
            tempTarget[0].Damage(2 + GetBonusDamage());
        }
        else if (cards[0].Equals(options[1]))
        {
            tempTarget[0].Damage(1 + GetBonusDamage());
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
        //TODO reset options when sharpen would reset
        base.sharpen();
        for (int i = 0; i < GetBonusDamage(); i++)
        {
            options[0].sharpen();
            options[1].sharpen();
        }
    }
}
