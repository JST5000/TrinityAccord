using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static EnemyData Interpret(string enemyName)
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
                Debug.Log(Interpret(name).EnemyName);

            }
            catch (KeyNotFoundException e)
            {
                Debug.Log("Did not find Key '" + name + "'");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
