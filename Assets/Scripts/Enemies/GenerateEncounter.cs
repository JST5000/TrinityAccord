using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Level { TUTORIAL, ONE, TWO, THREE, FOUR, BOSS };

public class GenerateEncounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static EnemyData[] GetEncounter(Level level)
    {
        if (level == Level.TUTORIAL)
        {
            EnemyData[] encounter = { new Rhino(), new Rhino(), new Rhino() };
            return encounter;
        }
        throw new System.Exception("Unable to generate an encounter for this level " + level);
    }
}
