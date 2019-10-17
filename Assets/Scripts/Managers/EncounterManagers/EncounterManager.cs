using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EncounterManager : MonoBehaviour
{
    public static int QueuedIncome { get; set; } = 0;

    EnemyData[] originalEncounter;
    public EnemyManager[] allEnemyManagers;
    public GameObject[] enemyGameObjects;
    public int enemyCount = 0;
    public Image VictorySplash;
    public TextMeshProUGUI IncomeStatement;

    public Transform Choose3Menu;

    public Image EnemyTurnSplash;

    float timeUntilVictory = 0;

    private EnemyManager targetedEnemy = null;

    public bool AllDamageStuns { get; set; } = false;

    public static EncounterManager Get()
    {
        return GameObject.Find("Board")?.GetComponent<EncounterManager>();
    }

    //Allows static access to the Spawn functionality for enemies to call in their attacks
    //Abstracts knowledge of what object has the EncounterManager script
    public static void SpawnEnemyInDefaultManager(EnemyData newEnemy)
    {
        Get().SpawnEnemy(newEnemy);
        UpdateAllEnemyUI();
    }

    public void Init(EnemyData[] encounter)
    {

        enemyGameObjects = new GameObject[5];
        enemyGameObjects[0] = (GameObject.Find("Enemy 1"));
        enemyGameObjects[1] = (GameObject.Find("Enemy 2"));
        enemyGameObjects[2] = (GameObject.Find("Enemy 3"));
        enemyGameObjects[3] = (GameObject.Find("Enemy 4"));
        enemyGameObjects[4] = (GameObject.Find("Enemy 5"));

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

        UpdateAllEnemyUI();
    }

    public EnemyManager GetRandomAliveEnemyManager()
    {
            List<int> validEnemyIndices = new List<int>();
            for (int i = 0; i < allEnemyManagers.Length; ++i)
            {
                if (allEnemyManagers[i].IsAlive())
                {
                    validEnemyIndices.Add(i);
                }
            }
            if (validEnemyIndices.Count == 0)
            {
                return null;
            }
            int randomIndex = UnityEngine.Random.Range(0, validEnemyIndices.Count);
            return allEnemyManagers[validEnemyIndices[randomIndex]];
    }

    public void EndTurn()
    {
        StartCoroutine(EndTurnWithTiming());
    }

    private IEnumerator EndTurnWithTiming()
    {
        HandManager.Get().DisableHandInteractions();
        CanvasGroupManip.Enable(EnemyTurnSplash.GetComponent<CanvasGroup>());
        AllDamageStuns = false;

        float totalEnemyTurn = .8f;

        yield return WaitForNSeconds(totalEnemyTurn / 2);

        EndTurnForEachEnemy();

        yield return WaitForNSeconds(totalEnemyTurn / 2);

        CanvasGroupManip.Disable(EnemyTurnSplash.GetComponent<CanvasGroup>());
        HandManager.Get().EnableHandInteraction();

        SetTargetedEnemy(null);

        //Must happen after the enemy attack so scythe soldier can use the results
        StackManager.Get().ResetCounts();
    }

    private void EndTurnForEachEnemy()
    {
        List<EnemyManager> validEnemies = new List<EnemyManager>();
        //Only end turn for legitimate enemies (Prevents spawns from getting a "Free" turn on spawn)
        foreach (EnemyManager enemyMan in allEnemyManagers)
        {
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
        UpdateAllEnemyUI();
    }

    IEnumerator WaitForNSeconds(float n)
    {
        yield return new WaitForSeconds(n);
    }

    public void OnEncounterWin()
    {
        PermanentState.Wins++;
        HandManager hand = HandManager.Get();
        hand.DisableHandInteractions();
        //No more turns, so disallow ending turn
        GameObject.Find("EndTurnButton").GetComponent<Button>().interactable = false;

        PermanentState.PushEncounterData();

        AddIncomeAndUpdateUI();
        CanvasGroupManip.Enable(VictorySplash.GetComponent<CanvasGroup>());
        timeUntilVictory = 1.5f;
    }

    private void AddIncomeAndUpdateUI()
    {
        int income;
        if (PermanentState.Wins <= 3)
        {
            income = 2;
        }
        else
        {
            income = 3;
        }
        if(PermanentState.FightWasHarder)
        {
            income++;
        }

        income += QueuedIncome;
        QueuedIncome = 0;

        PermanentState.Money += income;
        IncomeStatement.text = "+" + income + " Coins!";
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

            SpawnRandomGoldfish();

            AllDamageStuns = false;
        } else
        {
            Debug.LogError("No encounter stored in PermanentState.nextEncounter! Unable to create encounter.");
        }
    }

    /// <summary>
    /// Random treasure event!
    /// </summary>
    private void SpawnRandomGoldfish()
    {
        if(Random.Range(0, 8) == 0)
        {
            SpawnEnemy(new Goldfish());
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
                OnVictoryScreenFinished();
            }
        }
    }

    private void OnVictoryScreenFinished()
    {
        CanvasGroupManip.Disable(VictorySplash.GetComponent<CanvasGroup>());

        //Hand does not get turned back on, reset of scene will restore it.

        if (PermanentState.Wins >= 6)
        {
            SceneManager.LoadScene("WinScreen");
        } else
        {

        }
        Instantiate(Choose3Menu, GameObject.Find("Canvas").transform, false);
    }

    public void SetTargetInidcators(EnemyManager[] targets)
    {
        foreach(EnemyManager enemy in allEnemyManagers)
        {
            enemy.SetTargetIndicator(false);
        }
        foreach(EnemyManager target in targets)
        {
            target.SetTargetIndicator(true);
        }
    }

    public void SetTargetedEnemy(EnemyManager newTarget)
    {
        targetedEnemy = newTarget;
    }

    public EnemyManager GetTargetedEnemy()
    {
        if(targetedEnemy != null && !targetedEnemy.IsEmpty())
        {
            return targetedEnemy;
        } else
        {
            return null;
        }
    }

    public static void UpdateAllEnemyUI()
    {
        foreach (EnemyManager man in Get().allEnemyManagers)
        {
            if (!man.IsEmpty())
            {
                man.UpdateUIData();
            }
        }
    }

    public static List<EnemyManager> GetEnemyManagersWithName(string enemyName)
    {
        List<EnemyManager> enemy = new List<EnemyManager>();
        if (Get() != null)
        {
            foreach (EnemyManager man in Get().allEnemyManagers)
            {
                if (!man.IsEmpty() && man.GetUIData().EnemyName.Equals(enemyName))
                {
                    enemy.Add(man);
                }
            }
            return enemy;
        }
        return null;
    }
}
