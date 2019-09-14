using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualWield : CardData
{
    public DualWield()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
       return new UICardData("DualWield", cost: 2, "Deal " + GetDamage() + " damage to target, random card in hand costs 1 until end of encounter", UICardData.CardType.ATTACK);
    }

    private int GetDamage()
    {
        return 4 + GetBonusDamage();
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(GetDamage());
        CardManager card = DeckManager.Get().GetRandomValidCardManagerFromHand();
        if (card != null)
        {
            card.GetCardData().SetCost(1);
        }

        //Ensure clickability of cards is updated
        HandManager.Get().UpdateAllCardsInHand();
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
