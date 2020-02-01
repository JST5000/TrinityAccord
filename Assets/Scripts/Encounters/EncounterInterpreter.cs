using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using CsvHelper;
using System.IO;
using System.Text.RegularExpressions;

public class EncounterInterpreter
{

    public static Dictionary<string, EnemyData> nameToEnemy;

    private static Dictionary<string, int> averageDamagesCeil = null;


    public static Dictionary<string, EnemyData> GetNameDictionary(EnemyData[] allEnemies)
    {
        Dictionary<string, EnemyData> nToE = new Dictionary<string, EnemyData>();
        for(int i = 0; i < allEnemies.Length; ++i)
        {
            nToE[allEnemies[i].EnemyName] = allEnemies[i];
            foreach(string altName in allEnemies[i].AlternateNames)
            {
                nToE[altName] = allEnemies[i];
            }
        }
        return nToE;
    }

    //TODO maintain this list
    public static EnemyData[] GetAllEnemies()
    {
        List<EnemyData> enemies = new List<EnemyData>();
        enemies.Add(new Axe());
        enemies.Add(new Boar());
        enemies.Add(new Doombringer());
        enemies.Add(new Rhino());
        enemies.Add(new Spirit());
        enemies.Add(new Squirrel());
        enemies.Add(new Swordman());
        enemies.Add(new Squid());
        enemies.Add(new Turtle());
        enemies.Add(new Knight());
        enemies.Add(new Hive());
        enemies.Add(new Wasp());
        enemies.Add(new Tiger());
        enemies.Add(new Berserker());
        enemies.Add(new Shark());
        enemies.Add(new GangLeader());
        enemies.Add(new GangWimp());
        enemies.Add(new ScytheSoldier());
        enemies.Add(new Mole());
        enemies.Add(new Executioner());
        enemies.Add(new Clam());
        enemies.Add(new Piranha());
        enemies.Add(new Pufferfish());
        enemies.Add(new Whale());
        enemies.Add(new AttackBarnacle());
        enemies.Add(new DefenseBarnacle());
        enemies.Add(new Dung());
        enemies.Add(new Fly());
        enemies.Add(new Bait());
        enemies.Add(new Bull());
        enemies.Add(new Crab());
        enemies.Add(new Goldfish());
        enemies.Add(new Owl());
        enemies.Add(new Egg());
        enemies.Add(new Fledgling());
        return enemies.ToArray();
    }

    public static EnemyData InterpretWord(string enemyName)
    {
        if (nameToEnemy == null)
        {
            nameToEnemy = GetNameDictionary(GetAllEnemies());
        }

        if (nameToEnemy.TryGetValue(enemyName, out EnemyData enemyType))
        {
            return enemyType.Clone();
        } else
        {
            return null;
        }
    }

    private static void TestInterpret()
    {
        Debug.Log("Testing Interpret! (Expected = Invalid, Names, Invalid)");
        string[] test = { "Invalid", "Sword", "Axe", "Boar", "Rhino", "Spirit", "Doombringer", "Squirrel", "" };
        foreach (string name in test)
        {
            try
            {
                Debug.Log(InterpretWord(name).EnemyName);

            }
            catch (KeyNotFoundException)
            {
                Debug.Log("Did not find Key '" + name + "'");
            }
        }
    }

    public static EncounterData GetEncounterFromText(string encounterList)
    {
        EncounterData encounter = new EncounterData();
        encounter.Encounter = encounterList;

        SetAverageDamage(ref encounter);

        return encounter;
    }
    
    //Throws KeyNotFoundException if input is invalid. 
    public static EnemyData[] GetEnemiesToFight(string input)
    {
        
        Debug.Log("Text being interpretted: " + input);
        string[] separator = { ", " };
        string[] split = input.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
        List<EnemyData> encounter = new List<EnemyData>();
        foreach (string enemyName in split)
        {
            /* Useful debug info
             * for(int i = 0; i < enemyName.Length; ++i)
            {
                Debug.Log(enemyName[i]);
            } */
            //Debug.Log("Split Text: " + enemyName);
            EnemyData result = InterpretWord(enemyName.Trim());
    
            encounter.Add(result);
        }
        return encounter.ToArray();
    }

    public static List<EncounterData> ReadInEncounters()
    {
        string encounterListFilePath = "EncountersGenerated";
        TextAsset encounterData = Resources.Load<TextAsset>(encounterListFilePath);

        string[] data = encounterData.text.Split(new char[] { '\n' });

        List<EncounterData> encounters = new List<EncounterData>();
        Dictionary<string, int> averageDamages = GetAverageDamagesCeil();

        //Starts from 1 since the first row is headers which are not used in this implementation
        for (int i = 1; i < data.Length; ++i)
        {
            string[] row = SplitCSVRow(data[i]);
            if (row.Length <= 1) { continue; } //No data, empty newline
            EncounterData encounter = new EncounterData();
            if (row.Length != encounter.GetType().GetProperties().Length - 1)
            {
                Debug.LogError("Some data is missing on load in " + encounterListFilePath + ". This likely means a new column was added that is not being interpreted!");
            }

            int intData = 0;
            int.TryParse(TrimQuotes(row[0].Trim()), out intData);
            encounter.Level = intData;

            int difficulty = 0;
            int.TryParse(TrimQuotes(row[1].Trim()), out difficulty);
            encounter.Difficulty = difficulty;

            encounter.Damage = TrimQuotes(row[2].Trim());
            encounter.Encounter = TrimQuotes(row[3].Trim());

            SetAverageDamage(ref encounter);

            encounters.Add(encounter);
        }
        return encounters;
    }

    private static Dictionary<string, int> GetAverageDamagesCeil()
    {
        if (averageDamagesCeil != null)
        {
            return averageDamagesCeil;
        }
        else
        {
            averageDamagesCeil = new Dictionary<string, int>();

            string averageDamageFilePath = "encounter_history";
            TextAsset averageDamageData = Resources.Load<TextAsset>(averageDamageFilePath);
            string[] data = averageDamageData.text.Split(new char[] { '\n' });
            for (int i = 0; i < data.Length; ++i)
            {
                string[] row = SplitCSVRow(data[i]);
                if (row.Length < 2) continue;

                float.TryParse(TrimQuotes(row[1]), out float averageDamage);
                averageDamagesCeil[TrimQuotes(row[0].Trim())] = Mathf.CeilToInt(averageDamage);
            }
        }

        return averageDamagesCeil;
    }

    /// <summary>
    /// Requires that encounter already have it's list of enemies set
    /// </summary>
    /// <param name="encounter"></param>
    private static void SetAverageDamage(ref EncounterData encounter)
    {
        if (GetAverageDamagesCeil().TryGetValue(encounter.Encounter, out int average))
        {
            encounter.AverageDamageRoundedUp = average;
        }
        else
        {
            //Heuristic for fights that don't have recorded averages for damage yet
            int count = encounter.Encounter.Split(',').Length;
            encounter.AverageDamageRoundedUp = 2 * count;
        }
    }

    private static string[] SplitCSVRow(string csvRow)
    {
        return Regex.Split(csvRow, "," + "(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

    }

    public static void WriteEncounterData(List<EncounterData> allData)
    {
        string csvFilePath = "Assets\\Resources\\EncountersGenerated.csv";
        using (StreamWriter writable = new StreamWriter(csvFilePath, false))
        {
            writable.WriteLine(EncounterData.GetCSVFieldNames());
            for(int i = 0; i < allData.Count; ++i) 
            {
                EncounterData data = allData[i];
                string line = data.WriteCSVLine();
                writable.WriteLine(line);               
            }
        }

    }

    public static string TrimQuotes(string input)
    {
        int start = 0;
        int length = input.Length;
        if (input.Length != 0 && input[0] == '"')
        {
            start++;
            length--;
        }
        if(input.Length >= 2 && input[input.Length - 1] == '"')
        {
            length--;
        }
        return input.Substring(start, length);
    }
}
