using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolvingContagion : CardData
{
    int mode;
    public EvolvingContagion()
    {
        setMode();
    }
    public void setMode()
    {
        mode = UnityEngine.Random.Range(1, 4);
        switch (mode)
        {
            case 1:
                cardData = new UICardData("Greed", cost: 0, "Draw 2", UICardData.CardType.SPELL);
                target = Target.BOARD;
                cost = 0;
                break;
            case 2:
                cardData = new UICardData("Power", cost: 0, "Gain 2 energy", UICardData.CardType.SPELL);
                target = Target.BOARD;
                cost = 0;
                break;
            case 3:
                cardData = new UICardData("Vile Sword", cost: 2, "Deal 5 damage", UICardData.CardType.ATTACK);
                cost = 2;
                target = Target.ENEMY;
                break;

        }

    }

    public override void Action(EnemyManager[] enemys)
    {
        switch (mode)
        {
            case 1:
                draw();
                draw();
                break;
            case 2:
                addEnergy(2);
                break;
            case 3:
                enemys[0].Damage(5);
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

    public override void onDiscard()
    {
        setMode();
    }
    public override int SecondAction(CardManager card)
    {
        throw new System.NotImplementedException();
    }
}
