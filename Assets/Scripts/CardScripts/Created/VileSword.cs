using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VileSword : CardData
{
    public VileSword()
    {
        cardData = new UICardData("VileSword", cost: 2, "Deal 5 damage", UICardData.CardType.ATTACK, "Vile_Sword");
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(5+sharpened);
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
        cardData = new UICardData("VileSword", cost: 1, "Deal " + (5 + sharpened) + " damage.", UICardData.CardType.ATTACK);
    }
}
