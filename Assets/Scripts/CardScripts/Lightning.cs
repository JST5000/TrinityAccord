using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : CardData
{

    public Lightning()
    {
        //Using state since a card may be modified (Ex. Feather Blade changing cost)
        cardData = new UICardData("Lightning", cost: 3, "Deal 3 damage to 3 random enemies.", UICardData.CardType.SPELL);
        cost = 3;
        target = Target.ALL_ENEMIES;
    }


    //Needs all enemies
    public override void Action(EnemyManager[] enemies)
    {
        for (int i = 0; i < 3;)
        {
            damageRandom(3);
        }
        //TODO requery for enemies after each hit, incase someone dies and you need to recalculate.
        //A new enemy may be spawned/removed based on a non-lightning effect so we cannot deduce from our set the new set of targets.
    }

    public override void Action(CardData[] cards)
    {
        throw new System.NotImplementedException("Lightning does not target cards");
    }

    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {
        throw new System.NotImplementedException("Lightning does not target cards and enemies at the same time.");
    }

    public override int SecondAction(CardManager card)
    {
        throw new System.NotImplementedException();
    }
}
