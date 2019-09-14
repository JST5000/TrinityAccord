using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTonHammer : CardData
{
    public OneTonHammer()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("One-Ton-Hammer", 
            cost: 3, 
            "Deal " + GetDamage() + " damage to target, random card in hand costs 3 until end of encounter", 
            UICardData.CardType.ATTACK, 
            "One_Ton_Hammer");
    }

    private int GetDamage()
    {
        return 9 + GetBonusDamage();
    }


    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(GetDamage());
        CardManager[] hand = getHand();
        List<int> validCards = new List<int>();
        for (int i = 0; i < hand.Length; ++i)
        {
            if (!hand[i].IsEmpty())
            {
                validCards.Add(i);
            }
        }
        if (validCards.Count == 0)
        {
            return;
        }
        int randomIndex = UnityEngine.Random.Range(0, validCards.Count);
        hand[validCards[randomIndex]].GetCardData().SetCost(3);
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
