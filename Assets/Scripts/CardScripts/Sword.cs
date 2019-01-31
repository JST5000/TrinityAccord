using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : CardData
{
    public override string CardName()
    {
        return "Sword";
    }

    public override int Cost()
    {
        return 2;
    }

    public override int Target()
    {
        return (int)Targets.Enemies;
    }
    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(3);
    }
    public override void Action(CardData[] cards)
    {

    }
    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {

    }


}

