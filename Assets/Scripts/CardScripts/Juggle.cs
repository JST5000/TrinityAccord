using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juggle : CardData
{
    private static int maxHits = 4;

    public Juggle()
    {
        target = Target.ALL_ENEMIES;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Juggle", cost: 1, $"Deal " + GetDamage() + $" damage to a random enemy, repeat if the enemy is stunned after. (Max {maxHits} hits)"
            , UICardData.CardType.ATTACK, cardArtFileName: "Juggle");
    }

    private int GetDamage()
    {
        return 2 + sharpenDamage;
    }

    public override void Action(EnemyManager[] enemys)
    {
        SoundManager.PlayCardSFX("Juggle1");

        bool targetIsStunned = true;
        int hits = 0;
        while (hits < maxHits && targetIsStunned)
        {
            EnemyManager enemy = EncounterManager.Get().GetRandomAliveEnemyManager();
            if (enemy)
            {
                enemy.Damage(GetDamage());
                targetIsStunned = !enemy.IsAlive() || enemy.IsStunned();
                ++hits;
            } else
            {
                //Enemy given doesn't exist, so you are done hitting things
                break;
            }
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
