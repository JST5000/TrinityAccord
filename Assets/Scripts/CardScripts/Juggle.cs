using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juggle : CardData
{
    public Juggle()
    {
        cardData = new UICardData("Juggle", cost: 1, "If there is spell in hand, deal 3 damage and stun an enemy", UICardData.CardType.ATTACK, cardArtFileName: "Juggle");
        target = Target.ENEMY;
    }

    public override void Action(EnemyManager[] enemys)
    {
        SoundManager.playSound("Juggle1");
        enemys[0].Damage(3+sharpened);
        enemys[0].Stun();
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
    public override bool IsPlayableAdditionalRequirements()
    {

        foreach (CardManager cardManager in getHand())
        {
                if (!cardManager.IsEmpty() && cardManager.GetCardData()!=null&&cardManager.GetCardData().getType().Equals(UICardData.CardType.SPELL))
                    return true;
        }
        return false;
        
    }
    public override void sharpen()
    {
        sharpened++;
        cardData = new UICardData("Juggle", cost: 1, "If there is spell in hand, deal " + (1 + sharpened) + " damage and stun an enemy", UICardData.CardType.ATTACK);
    }
}
