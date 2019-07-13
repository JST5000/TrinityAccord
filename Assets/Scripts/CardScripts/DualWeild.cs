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
        return 4 + sharpenDamage;
    }

    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(GetDamage());
        /*CardManager[] hand = getHand();
        List<int> validCards = new List<int>();
        for (int i = 0; i < hand.Length; ++i)
        {
            if (!hand[i].IsEmpty())
            {
                validCards.Add(i);
            }
        }
        //No cards in hand
        if (validCards.Count == 0)
        {
            return;
        }
        int randomIndex = UnityEngine.Random.Range(0, validCards.Count); */
        CardManager card = DeckManager.Get().GetRandomValidCardManagerFromHand();
        if (card != null)
        {
            card.GetCardData().setCost(1);
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
