using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public DeckManager deckManager;
    public EncounterManager encounterManager;
    // Start is called before the first frame update
    ArrayList deck;
    void Start()
    {
        GenerateTest();
        InitializeDeck();
        InitializeEncounter();
    }

    private void InitializeDeck()
    {
        //GameObject.Find("Canvas/Deck").GetComponent<DeckManager>().Init(deck);
        //GetComponentInChildren<DeckManager>().Init((ArrayList)deck.Clone());
        deckManager.Init((ArrayList)deck.Clone());
    } 
    private void GenerateTest()
    {
        deck = new ArrayList();
        deck.Add(new Dagger());
        deck.Add(new Dagger());
        deck.Add(new Dagger());
        deck.Add(new Dagger());
        deck.Add(new Sword());
        deck.Add(new Sword());
        deck.Add(new Lightning()); //Added to see variety, should replace with Energize + class card - Jackson
    }

    private void InitializeEncounter()
    {
        encounterManager.Init(GenerateEncounter.GetEncounter(Level.TUTORIAL));
    }

}
