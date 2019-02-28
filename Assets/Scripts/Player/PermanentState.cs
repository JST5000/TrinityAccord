using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentState : MonoBehaviour
{

    //Player deck - State should hold for multiple encounters
    public static List<CardData> playerDeck;

    public static void AddCardToPlayerDeckList(CardData card)
    {
        playerDeck.Add(card);
    }

    void Awake()
    {
        CreateSingleton();
        InitializeBaseDeck();
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

    private List<CardData> GetBaseDeck()
    {
        List<CardData> deck = new List<CardData>();
        deck.Add(new Dagger());
        deck.Add(new Dagger());
        deck.Add(new Dagger());
        deck.Add(new Dagger());
        deck.Add(new Sword());
        deck.Add(new Sword());
        deck.Add(new Shock());
        deck.Add(new Energize());
        //Added to see variety, should replace with Energize + class card - Jackson
        return deck;
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
