using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaos : CardData
{

    public Chaos()
    {
        cardData = new UICardData("Chaos", cost: 1, "Deal 1 damage at random for each card played before this", UICardData.CardType.ATTACK);
        target = Target.ALL_ENEMIES;
    }


    //Needs all enemies
    public override void Action(EnemyManager[] enemies)
    {
        for (int i = 0; i < getNumberOfCardsPlayed()-1; ++i)
        {
            damageRandom(1);
        }
        for (int i = 0; i < sharpened; ++i)
        {
            damageRandom(1);
        }
        //TODO requery for enemies after each hit, incase someone dies and you need to recalculate.
        //A new enemy may be spawned/removed based on a non-lightning effect so we cannot deduce from our set the new set of targets.
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
    public override void sharpen()
    {
        sharpened++;
        cardData = new UICardData("Chaos", cost: 1, "Deal 1 damage at random for each card played before this,+ "+sharpened, UICardData.CardType.ATTACK);
    }
}
