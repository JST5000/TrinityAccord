using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juggle : CardData
{
    public Juggle()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Juggle", cost: 1, $"Deal {GetDamage()} damage; if you have a spell in hand stun the enemy", UICardData.CardType.ATTACK, cardArtFileName: "Juggle");
    }

    private int GetDamage()
    {
        return 2 + GetBonusDamage();
    }


    public override void Action(EnemyManager[] enemys)
    {
        SoundManager.PlayCardSFX("Juggle1");
        enemys[0].Damage(GetDamage());

        //TODO, this will fail if we add queuing since it checks at the hit time, instead of cast time.
        if (HasSpellInHand())
        {
            enemys[0].Stun();
        }
    }

    private bool HasSpellInHand()
    {
        foreach (CardManager cardManager in getHand())
        {
            if (!cardManager.IsEmpty() && cardManager.GetCardData() != null && cardManager.GetCardData().GetTypeOfCard().Equals(UICardData.CardType.SPELL))
                return true;
        }
        return false;
    }

    public override string GetBonusEffectDescription()
    {
        if(HasSpellInHand())
        {
            return "+ Stun";
        } else
        {
            return "";
        }
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
