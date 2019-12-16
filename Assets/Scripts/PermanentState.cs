using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentState : MonoBehaviour
{

    //Player deck - State should hold for multiple encounters
    public static List<CardData> PlayerDeck { get; set; }
    public static List<CardData> QueuedCards { get; set; } = new List<CardData>();
    public static Level ExpectedLevel { get; set; }
    
    public static int Wins { get; set; } = 0;
    public static int Money { get; set; } = 10;
    public static int MaxHealth { get; set; } = 10;
    public static int Health { get; set; } = MaxHealth - 1;
    public static bool FightWasHarder { get; private set; } = false;

    private string previousTown = "";

    public static bool hasDraftedClassCard = false;

    public static bool PauseGameInteraction = false;

    public static WorldMap worldMap = new WorldMap();

    //Needs to be remembered between level loads
    public static List<int> unusedQuotes;

    private static int FinalFight = 5;
    
    private static EnemyData[] nextEncounter;

    //TODO refactor this to pull the stored fields and transformers out of this class
    private static DataPusher dataPusher;
    private static string runId;
    private static int startingHP;
    private static int endingHP;
    private static string encounterList;

    public static void AddCardToPlayerDeckList(CardData card)
    {
        QueuedCards.Add(card);
        ConsumeQueue();
    }

    public static GameObject Get()
    {
        return GameObject.Find("PermanentState");
    }

    private static void ConsumeQueue()
    {
        if(PlayerDeck != null)
        {
            PlayerDeck.AddRange(QueuedCards);
            QueuedCards.Clear();
        }
    }

    public static void ResetStatics()
    {
        Wins = 0;
        PermanentState.hasDraftedClassCard = false;
        InitializeBaseDeck();
        ExpectedLevel = Level.TUTORIAL;
        InitializeDefaultEncounter();
        ExpectedLevel = Level.ONE;
        Money = 0;
        Health = MaxHealth;
        unusedQuotes = new List<int>();
        worldMap = new WorldMap();
        FightWasHarder = false;
        runId = DataPusher.GetNewRunId();
        //Fixes test data leaving -1 in the data
        startingHP = Health;
        EncounterManager.QueuedIncome = 0; //So you can't kill a Goldfish then die and cash out
    }

    void Awake()
    {
        CreateSingleton();
        if(PlayerDeck == null)
        {
            ResetStatics();
            Health = MaxHealth;
        }
        PlayerDeck = CardDataUtil.CreateFreshCopiesOf(PlayerDeck); //Removes any buffs/debuffs
    }

    private void CreateSingleton()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("permanentState");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private static void InitializeBaseDeck()
    {
        PlayerDeck = new List<CardData>(GetBaseDeck());
        ConsumeQueue();
    }

    //For experimental builds
    private static List<CardData> GetExperimentalDeck()
    {
        List<CardData> deck = new List<CardData>();
        deck.Add(new Clone());
        deck.Add(new Wand());
        deck.Add(new Wand());
        deck.Add(new Wand());
        deck.Add(new Clone());
        return deck;
    }

    private static List<CardData> GetBaseDeck()
    {
        List<CardData> deck = new List<CardData>();
        deck.Add(new Sword());
        deck.Add(new Sword());
        deck.Add(new Dagger());
        deck.Add(new Dagger());
        deck.Add(new Dagger());
        deck.Add(new Dagger());
        deck.Add(new Energize());
        return deck;
    }


    private static void InitializeDefaultEncounter()
    {
        SetNextEncounter(GenerateEncounter.GetEncounter(ExpectedLevel));
    }


    //Returns the encounter
    public static EnemyData[] GetNextEncounter()
    {
        if(nextEncounter == null) { return null; }
        List<EnemyData> toCopy = new List<EnemyData>(nextEncounter);
        return Clone(toCopy).ToArray();
    }

    public static List<EnemyData> Clone(List<EnemyData> data)
    {
        List<EnemyData> copy = new List<EnemyData>();
        foreach(EnemyData enemy in data)
        {
            copy.Add(enemy.Clone());
        }
        return copy;
    }

    public static void SetNextEncounter(EnemyData[] next)
    {
        nextEncounter = next;
        
        //Add the properly formatted encounter list
        encounterList = "";
        for(int i = 0; i < next.Length; ++i)
        {
            encounterList += next[i];
            if(i != next.Length - 1)
            {
                encounterList += ", ";
            }
        }

        startingHP = Health;
    }

    /// <summary>
    /// Must be called immediately after a fight concludes (Win or lose)
    /// </summary>
    public static void PushEncounterData()
    {
        if (!Application.isEditor)
        {
            Get().GetComponent<DataPusher>().PushEncounterHistory(runId, encounterList, startingHP, Health, PlayerDeck);
        }
    }

    public static string GetFightTitle()
    {
        if (PermanentState.Wins < FinalFight)
        {
            string levelCount = "Level " + (1 + PermanentState.Wins);

            //Show the extra difficulty!
            if (FightWasHarder)
            {
                levelCount += "!!!";
            }
            return levelCount;
        }
        else
        {
            return "Final Boss!!!";
        }
    }



    public static string GetNextTownSceneName()
    {
        return worldMap.GetCurrentTown().SceneName;
    }

    public static void MoveToNextTown(bool left)
    {
        worldMap.MoveToNextTown(left);
    }

    public static void ChooseNextFight(bool increasedDifficulty)
    {
        Level toSelectFrom = ExpectedLevel;
        if (increasedDifficulty)
        {
            toSelectFrom = ExpectedLevel + 1;
        }

        FightWasHarder = increasedDifficulty;

        EnemyData[] selectedEncounter = GenerateEncounter.GetEncounter(toSelectFrom);
        SetNextEncounter(selectedEncounter);

        ExpectedLevel = GenerateEncounter.GetHarder(ExpectedLevel);
    }
}
