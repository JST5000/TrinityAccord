using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentState : MonoBehaviour
{

    //Player deck - State should hold for multiple encounters
    public static List<CardData> playerDeck;

    private static EnemyData[] nextEncounter;

    public static void AddCardToPlayerDeckList(CardData card)
    {
        playerDeck.Add(card);
    }

    void Awake()
    {
        CreateSingleton();
        if (playerDeck == null)
        {
            InitializeBaseDeck();
        }
        if(GetNextEncounter() == null) { 
            InitializeDefaultEncounter();
        }
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

    private void InitializeBaseDeck()
    {
        if (playerDeck == null)
        {
            playerDeck = new List<CardData>(GetBaseDeck());
        }
    }

    //For experimental builds
    private List<CardData> GetExperimentalDeck()
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

    private List<CardData> GetBaseDeck()
    {
        List<CardData> deck = new List<CardData>();
        deck.Add(new Sword());
        deck.Add(new Sword());
        deck.Add(new Dagger());
        deck.Add(new Dagger());
        deck.Add(new Dagger());
        deck.Add(new Dagger());
        deck.Add(new Energize());
        deck.Add(new Lightning());
        return deck;
    }


    private void InitializeDefaultEncounter()
    {
        SetNextEncounter(GenerateEncounter.GetEncounter(Level.TUTORIAL));
    }


    //Returns the encounter and sets it to null
    public static EnemyData[] GetNextEncounter()
    {       
        return nextEncounter;
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
