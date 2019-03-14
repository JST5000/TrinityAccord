using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using CsvHelper;
using System.IO;

public class EncounterInterpreter : MonoBehaviour
{

    public static string[] AllNames;
    public static EnemyData[] AllEnemies;
    public static Dictionary<string, EnemyData> nameToEnemy;

    public static Dictionary<string, EnemyData> GetNameDictionary(EnemyData[] allEnemies)
    {
        Dictionary<string, EnemyData> nToE = new Dictionary<string, EnemyData>();
        for(int i = 0; i < allEnemies.Length; ++i)
        {
            nToE[allEnemies[i].EnemyName] = allEnemies[i];
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
        /*enemies.Add(new Axe());
        enemies.Add(new Axe());
        enemies.Add(new Axe());
        enemies.Add(new Axe());
        enemies.Add(new Axe());
        enemies.Add(new Axe()); */
        return enemies.ToArray();
    }

    public static EnemyData InterpretWord(string enemyName)
    {
        if (nameToEnemy == null)
        {
            nameToEnemy = GetNameDictionary(GetAllEnemies());
        }
        return EnemyData.Copy(nameToEnemy[enemyName]);
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
            catch (KeyNotFoundException e)
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
            for(int i = 0; i < enemyName.Length; ++i)
            {
                Debug.Log(enemyName[i]);
            }
            Debug.Log("Split Text: " + enemyName);
            EnemyData result = InterpretWord(enemyName.Trim());
            Debug.Log("Enemy Interpretted: " + result.EnemyName);
            encounter.Add(result);
        }
        return encounter.ToArray();
    }

    // Start is called before the first frame update
    void Start()
    {
       /* string csvFilePath = "Assets\\Resources\\Data\\Encounters.csv";
        using (var textReader = new StreamReader(csvFilePath))
        using (var reader = new CsvReader(textReader))
        {
            var data = reader.GetRecords<EncounterData>();
            foreach( var item in data )
            {
                Debug.Log(item.Encounter);
            }
        } */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
