using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    //TODO
    //ArrayList<EnemyManager> enemies;
    EnemyData[] originalEncounter;
    EnemyManager[] allEnemyManagers;

    public void Init(EnemyData[] encounter)
    {
        originalEncounter = encounter;
        if(allEnemyManagers == null)
        {
            InitEnemyManagers();
        }
        for(int i = 0; i < originalEncounter.Length && i < allEnemyManagers.Length; ++i)
        {
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

    // Start is called before the first frame update
    void Start()
    {
        InitEnemyManagers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
