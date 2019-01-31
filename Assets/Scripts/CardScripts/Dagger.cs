﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : CardData
{
    public override string CardName()
    {
        return "Dagger";
    }

    public override int Cost()
    {
        return 1;
    }

    public override int Target()
    {
        return (int)Targets.Enemies;
    }
    public override void Action(EnemyManager[] enemys)
    {
        enemys[0].Damage(1);
    }
    public override void Action(CardData[] cards)
    {

    }
    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {

    }


}
