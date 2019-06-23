using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentState : MonoBehaviour
{

    //Player deck - State should hold for multiple encounters
    public static List<CardData> playerDeck;
    public static List<CardData> queuedCards = new List<CardData>();
    public static Level expectedLevel;
    public static int wins = 0;
    public static int money = 10;
    public static int maxHealth = 10;
    public static int health = maxHealth - 1;

    public static bool hasDraftedClassCard = false;

    public static bool PauseGameInteraction = false;

    //Needs to be remembered between level loads
    public static List<int> unusedQuotes;

    private static int FinalFight = 5;
    
    private static EnemyData[] nextEncounter;

    public static void AddCardToPlayerDeckList(CardData card)
    {
        queuedCards.Add(card);
        ConsumeQueue();
    }

    private static void ConsumeQueue()
    {
        if(playerDeck != null)
        {
            playerDeck.AddRange(queuedCards);
            queuedCards.Clear();
        }
    }

    public static void ResetStatics()
    {
        wins = 0;
        PermanentState.hasDraftedClassCard = false;
        InitializeBaseDeck();
        expectedLevel = Level.TUTORIAL;
        InitializeDefaultEncounter();
        expectedLevel = Level.ONE;
        money = 0;
        health = maxHealth;
        unusedQuotes = new List<int>();
    }

    void Awake()
    {
        CreateSingleton();
        if(playerDeck == null)
        {
            ResetStatics();
            health = maxHealth;
        }
        playerDeck = CardDataUtil.CreateFreshCopiesOf(playerDeck); //Removes any buffs/debuffs
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
        playerDeck = new List<CardData>(GetBaseDeck());
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
        SetNextEncounter(GenerateEncounter.GetEncounter(expectedLevel));
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
    }

    public static string GetFightTitle()
    {
        if (PermanentState.wins < FinalFight)
        {
            return "Level " + (1 + PermanentState.wins);
        }
        else
        {
            return "Final Boss!";
        }
    }

    public static string GetNextTownSceneName()
    {
        if(wins == 2)
        {
            return "Town4";
        } else if(wins == 4)
        {
            return "Town3";
        } else if(wins == FinalFight)
        {
            return "Town2";
        } else
        {
            return "Town1";
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
