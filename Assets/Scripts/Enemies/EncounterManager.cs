﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    EnemyData[] originalEncounter;
    EnemyManager[] allEnemyManagers;
    int enemyCount = 0;

    public Transform Choose3Menu;


    //Allows static access to the Spawn functionality for enemies to call in their attacks
    //Abstracts knowledge of what object has the EncounterManager script
    public static void SpawnEnemyInDefaultManager(EnemyData newEnemy)
    {
        string ownerOfEncounterManager = "Board";
        GameObject.Find(ownerOfEncounterManager).GetComponent<EncounterManager>().SpawnEnemy(newEnemy);
    }

    public void Init(EnemyData[] encounter)
    {
        originalEncounter = encounter;
        if(allEnemyManagers == null)
        {
            InitEnemyManagers();
        }
        for(int i = 0; i < originalEncounter.Length && i < allEnemyManagers.Length; ++i)
        {
            enemyCount++;
            allEnemyManagers[i].Init(originalEncounter[i]);
        }
        if(originalEncounter.Length > allEnemyManagers.Length)
        {
            Debug.Log("There were more enemies in this encounter than EnemyManagers. Please implement how this feature should be.");
        }
    }

    public void EndTurn()
    {
        foreach(EnemyManager enemyMan in allEnemyManagers) {
            enemyMan.EndTurn();
        }
    }

    void InitEnemyManagers()
    {
        allEnemyManagers = GetComponentsInChildren<EnemyManager>();
    }

    public void SpawnEnemy(EnemyData newEnemy)
    {
        bool didNotSpawn = true;
        foreach(EnemyManager manager in allEnemyManagers)
        {
            if(manager.IsEmpty())
            {
                manager.Init(newEnemy);
                didNotSpawn = false;
                enemyCount++;
                break;
            }
        }
        if(didNotSpawn)
        {
            Debug.Log("Tried to spawn " + newEnemy + " but there was no space.");
        }
    }

    public void OnEnemyDeath()
    {
        enemyCount--;
        if(enemyCount == 0)
        {
            Instantiate(Choose3Menu, GameObject.Find("Canvas").transform, false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitEnemyManagers();
        InitializeEncounter();
    }

    private void InitializeEncounter()
    {
        Init(GenerateEncounter.GetEncounter(Level.ONE));
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
