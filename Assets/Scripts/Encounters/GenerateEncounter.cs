using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Level { TUTORIAL, ONE, TWO, THREE, FOUR, BOSS };



public static class GenerateEncounter
{
    private static bool initialized = false;
    private static List<EncounterData> tutorial = new List<EncounterData>();
    private static List<EncounterData> one = new List<EncounterData>();
    private static List<EncounterData> two = new List<EncounterData>();
    private static List<EncounterData> three = new List<EncounterData>();
    private static List<EncounterData> four = new List<EncounterData>();
    private static List<EncounterData> boss = new List<EncounterData>();
    private static List<EncounterData> allEncounters;

    private static int LevelToInt(Level lvl)
    {
        switch(lvl)
        {
            case Level.TUTORIAL:
                return 0;
            case Level.ONE:
                return 1;
            case Level.TWO:
                return 2;
            case Level.THREE:
                return 3;
            case Level.FOUR:
                return 4;
            case Level.BOSS:
                return 5;
            default:
                return -1;
        }
    }

    public static Level GetEasier(Level lvl)
    {
        return (Level)((int)Mathf.Max(LevelToInt(Level.TUTORIAL), LevelToInt(lvl) - 1));
    }

    public static Level GetHarder(Level lvl)
    {
        return (Level)Mathf.Min(LevelToInt(Level.BOSS), LevelToInt(lvl) + 1);
    }

    public static void InitEncounterLists()
    {
        if (!initialized)
        {
            allEncounters = EncounterInterpreter.ReadInEncounters();
            foreach (EncounterData enc in allEncounters)
            {
                switch (enc.Level)
                {
                    case 0:
                        tutorial.Add(enc);
                        break;
                    case 1:
                        one.Add(enc);
                        break;
                    case 2:
                        two.Add(enc);
                        break;
                    case 3:
                        three.Add(enc);
                        break;
                    case 4:
                        four.Add(enc);
                        break;
                    case 5:
                        boss.Add(enc);
                        break;
                    default:
                        Debug.LogError("No encounter list for level: " + enc.Level);
                        break;
                }
            }
            initialized = true;
        }
    }

    private static T GetRandom<T>(List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }


    public static void WriteEncounterData()
    {
        EncounterInterpreter.WriteEncounterData(allEncounters);
    }

    public static EnemyData[] RerollUntilValid(List<EncounterData> pool)
    {
        int max = 100;
        for (int i = 0; i < max; ++i) {
            try
            {
                EnemyData[] chosenEncounter = EncounterInterpreter.InterpretText(GetRandom(pool).Encounter);
                return chosenEncounter;
            } catch(KeyNotFoundException)
            {
                //Invalid encounter, try again
            }
        }
        throw new KeyNotFoundException("Unable to find a valid encounter in " + max + " attempts.");
    }

    public static EnemyData[] GetEncounter(Level level)
    {
        if (!initialized)
        {
            InitEncounterLists();
        }
        if (level == Level.TUTORIAL)
        {
            return RerollUntilValid(tutorial);
        }
        else if (level == Level.ONE)
        {
            return RerollUntilValid(one);
        }
        else if (level == Level.TWO)
        {
            return RerollUntilValid(two);
        }
        else if (level == Level.THREE)
        {
            return RerollUntilValid(three);
        }
        else if (level == Level.FOUR)
        {
            return RerollUntilValid(four);
        }
        else if (level == Level.BOSS)
        {
            //TODO, change as we get more "Boss" battles or move the boss battle zone. This is currently the fight I want as "Final Battle" - Jackson
            return EncounterInterpreter.InterpretText("Gang Leader, Scythe, Scythe");
            //return RerollUntilValid(boss);
        }

        throw new System.Exception("Unable to generate an encounter for this level " + level);
    }
}
