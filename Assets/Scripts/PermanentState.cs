using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentState : MonoBehaviour
{

    //Player deck - State should hold for multiple encounters
    public static List<CardData> playerDeck;
    public static Level expectedLevel;

    private static EnemyData[] nextEncounter;

    public static void AddCardToPlayerDeckList(CardData card)
    {
        playerDeck.Add(card);
    }

    public static void ResetStatics()
    {
        InitializeBaseDeck();
        expectedLevel = Level.TUTORIAL;
        InitializeDefaultEncounter();
    }

    void Awake()
    {
        CreateSingleton();
        if(playerDeck == null)
        {
            ResetStatics();
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
    }

    //For experimental builds
    private static List<CardData> GetExperimentalDeck()
    {
        List<CardData> deck = new List<CardData>();
        deck.Add(new Clone());
        deck.Add(new Wand());
        deck.Add(new Wand());
        deck.Add(new Duplicate());
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
        deck.Add(new Claws());
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
