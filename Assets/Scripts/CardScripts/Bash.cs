using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bash : CardData
{
    public Bash()
    {
        target = Target.BOARD;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Bash", cost: 1, "Stun a random enemy", UICardData.CardType.SPELL);
    }

    public override int GetBonusDamage()
    {
        return 0;
    }

    public override void Action(EnemyManager[] enemies)
    {

        List<int> validEnemies = new List<int>();
        for (int i = 0; i < enemies.Length; ++i)
        {
            if (!enemies[i].IsEmpty())
            {
                validEnemies.Add(i);
            }
        }
        //Nothing to damage
        if (validEnemies.Count == 0)
        {
            return;
        }
        int randomIndex = UnityEngine.Random.Range(0, validEnemies.Count);
        enemies[validEnemies[randomIndex]].Stun();

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
