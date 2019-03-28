using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using CsvHelper;
using System.IO;
using System.Text.RegularExpressions;

public class EncounterInterpreter
{

    public static Dictionary<string, EnemyData> nameToEnemy;

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
        return enemies.ToArray();
    }

    public static EnemyData InterpretWord(string enemyName)
    {
        if (nameToEnemy == null)
        {
            nameToEnemy = GetNameDictionary(GetAllEnemies());
        }
       
        return nameToEnemy[enemyName].Clone();
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
    
    //Throws KeyNotFoundException if input is invalid. 
    public static EnemyData[] InterpretText(string input)
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
            Debug.Log("Enemy Interpretted: " + result.EnemyName);
            encounter.Add(result);
        }
        return encounter.ToArray();
    }

    public static List<EncounterData> ReadInEncounters()
    {
        string csvFilePath = "Data\\EncountersGenerated";
        TextAsset encounterData = Resources.Load<TextAsset>(csvFilePath);

        string[] data = encounterData.text.Split(new char[] { '\n' });

        List<EncounterData> encounters = new List<EncounterData>();

        //Starts from 1 since the first row is headers which are not used in this implementation
        for (int i = 1; i < data.Length; ++i)
        {
            string[] row = Regex.Split(data[i], "," + "(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");// data[i].Split(new char[] { });
            if (row.Length <= 1) { continue; } //No data, empty newline
            EncounterData encounter = new EncounterData();
            if (row.Length != encounter.GetType().GetProperties().Length)
            {
                Debug.LogError("Some data is missing on load in " + csvFilePath + ". This likely means a new column was added that is not being interpreted!");
            }

            int intData = 0;
            int.TryParse(TrimQuotes(row[0].Trim()), out intData);
            encounter.Level = intData;

            int difficulty = 0;
            int.TryParse(TrimQuotes(row[1].Trim()), out difficulty);
            encounter.Difficulty = difficulty;

            encounter.Damage = TrimQuotes(row[2].Trim());
            encounter.Encounter = TrimQuotes(row[3].Trim());


            encounters.Add(encounter);
        }
        return encounters;
    }

    public static void WriteEncounterData(List<EncounterData> allData)
    {
        string csvFilePath = "Assets\\Resources\\Data\\EncountersGenerated.csv";
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
