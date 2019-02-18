using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Level { TUTORIAL, ONE, TWO, THREE, FOUR, BOSS };

public static class GenerateEncounter
{

    public static EnemyData[] GetEncounter(Level level)
    {
        if (level == Level.TUTORIAL)
        {
            EnemyData[] encounter = { new Swordman(), new Rhino(), new Axe() };
            return encounter;
        } else if(level == Level.ONE)
        {
            EnemyData[] encounter = { new Swordman(), new Boar(), new Axe() };
            return encounter;
        }
        throw new System.Exception("Unable to generate an encounter for this level " + level);
    }
}
