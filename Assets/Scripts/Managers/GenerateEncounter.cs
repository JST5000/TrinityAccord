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
            EnemyData[] encounter = { new Squirrel() };
            return encounter;
        } else if(level == Level.ONE)
        {
            EnemyData[] encounter = { new Swordman(), new Doombringer()};
            return encounter;
        } else if(level == Level.TWO)
        {
            EnemyData[] encounter = { new Boar(), new Rhino() };
            return encounter;
        } else if(level == Level.THREE)
        {
            EnemyData[] encounter = { new Doombringer(), new Rhino(), new Doombringer() };
            return encounter;
        } else if(level == Level.FOUR)
        {
            EnemyData[] encounter = { new Doombringer(), new Doombringer(), new Doombringer() };
            return encounter;
        } else if(level == Level.BOSS)
        {
            EnemyData[] encounter = { new Doombringer(), new Doombringer(), new Doombringer(), new Doombringer() };
            return encounter;
        }
        throw new System.Exception("Unable to generate an encounter for this level " + level);
    }
}
