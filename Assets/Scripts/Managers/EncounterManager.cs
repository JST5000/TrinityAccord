using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EncounterManager : MonoBehaviour
{
    EnemyData[] originalEncounter;
    public EnemyManager[] allEnemyManagers;
    public int enemyCount = 0;
    public Image VictorySplash;

    public Transform Choose3Menu;

    float timeUntilVictory = 0;

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
        if (allEnemyManagers == null)
        {
            InitEnemyManagers();
        }
        for (int i = 0; i < originalEncounter.Length && i < allEnemyManagers.Length; ++i)
        {
            enemyCount++;
            allEnemyManagers[i].Init(originalEncounter[i]);
        }
        if (originalEncounter.Length > allEnemyManagers.Length)
        {
            Debug.Log("There were more enemies in this encounter than EnemyManagers. Please implement how this feature should be.");
        }
    }

    public void EndTurn()
    {
        List<EnemyManager> validEnemies = new List<EnemyManager>();
        //Only end turn for legitimate enemies (Prevents spawns from getting a "Free" turn on spawn)
        foreach (EnemyManager enemyMan in allEnemyManagers) {
            if (!enemyMan.IsEmpty())
            {
                validEnemies.Add(enemyMan);
            }
        }
        foreach (EnemyManager validEnemy in validEnemies)
        {
            validEnemy.EndTurn();
        }
    }

    void InitEnemyManagers()
    {
        allEnemyManagers = GetComponentsInChildren<EnemyManager>();
    }

    public void SpawnEnemy(EnemyData newEnemy)
    {
        bool didNotSpawn = true;
        foreach (EnemyManager manager in allEnemyManagers)
        {
            if (manager.IsEmpty())
            {
                manager.Init(newEnemy);
                didNotSpawn = false;
                enemyCount++;
                break;
            }
        }
        if (didNotSpawn)
        {
            Debug.Log("Tried to spawn " + newEnemy + " but there was no space.");
        }
    }

    public void OnEnemyDeath()
    {
        enemyCount--;
        if (enemyCount == 0)
        {
            OnEncounterWin();
        }
    }

    IEnumerator WaitForNSeconds(float n)
    {
        yield return new WaitForSeconds(n);
    }

    public void OnEncounterWin()
    {
        PermanentState.wins++;
        HandManager hand = GameObject.Find("Hand").GetComponent<HandManager>();
        hand.DisableHandInteractions();
        //No more turns, so disallow ending turn
        GameObject.Find("EndTurnButton").GetComponent<Button>().interactable = false;

        CanvasGroupManip.Enable(VictorySplash.GetComponent<CanvasGroup>());
        timeUntilVictory = 1.5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitEnemyManagers();
        InitializeEncounter();
    }

    private void InitializeEncounter()
    {
        EnemyData[] nextEncounter = PermanentState.GetNextEncounter();
        if (nextEncounter != null)
        {
            Init(nextEncounter);
        } else
        {
            Debug.LogError("No encounter stored in PermanentState.nextEncounter! Unable to create encounter.");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(timeUntilVictory > 0)
        {
            timeUntilVictory -= Time.deltaTime;
            if(timeUntilVictory <= 0)
            {
                OnVictory();
            }
        }
    }

    private void OnVictory()
    {
        CanvasGroupManip.Disable(VictorySplash.GetComponent<CanvasGroup>());

        //Hand does not get turned back on, reset of scene will restore it.

        if (PermanentState.wins >= 6)
        {
            SceneManager.LoadScene("WinScreen");
        }
        Instantiate(Choose3Menu, GameObject.Find("Canvas").transform, false);
    }
}
