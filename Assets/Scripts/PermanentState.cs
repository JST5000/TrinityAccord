﻿using System.Collections;
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
        playerDeck = CreateFreshCopiesOf(playerDeck); //Removes any buffs/debuffs
    }

    private List<CardData> CreateFreshCopiesOf(List<CardData> deck)
    {
        List<CardData> fresh = new List<CardData>();
        foreach(CardData card in deck)
        {
            fresh.Add(card.Clone());
        }
        return fresh;
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
        deck.Add(new Claws());
        return deck;
    }


    private void InitializeDefaultEncounter()
    {
        SetNextEncounter(GenerateEncounter.GetEncounter(Level.TUTORIAL));
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
