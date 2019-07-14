using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandstorm : CardData
{
    public Sandstorm()
    {
        target = Target.ALL_ENEMIES;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Sandstorm", cost: 2, "Deal " + GetDamage() + " damage to all enemies, Preserve all cards in hand", UICardData.CardType.SPELL);
    }

    private int GetDamage()
    {
        return 1 + GetBonusDamage();
    }

    public override void Action(EnemyManager[] enemys)
    {
        foreach (EnemyManager enemy in enemys)
        {
            enemy.Damage(GetDamage());
        }
        foreach(CardManager man in DeckManager.Get().hand)
        {
            man.PreserveCard();
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
