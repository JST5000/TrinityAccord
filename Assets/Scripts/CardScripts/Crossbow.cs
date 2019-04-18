using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : CardData
{
    int mode;
    public Crossbow()
    {
        mode = 2;
        setMode();
    }
    public void setMode()
    {
        if (mode == 1)
        {
            mode = 2;
        }
        else
        {
            mode = 1;
        }
        switch (mode)
        {
            case 1:
                cardData = new UICardData("Crossbow", cost: 2, "Deal 6 damage Flip", UICardData.CardType.ATTACK);
                target = Target.ENEMY;
                cost = 2;
                break;
            case 2:
                cardData = new UICardData("Reload", cost: 0, "Flip", UICardData.CardType.SPELL);
                target = Target.BOARD;
                cost = 0;
                break;

        }

    }

    public override void Action(EnemyManager[] enemys)
    {
        switch (mode)
        {
            case 1:
                enemys[0].Damage(6);
                break;
            case 2:
                break;
        }
        setMode();
    }
    public override void Action(CardData[] cards)
    {

    }
    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {

    }

    public override int SecondAction(CardManager card)
    {
        throw new System.NotImplementedException();
    }
}
