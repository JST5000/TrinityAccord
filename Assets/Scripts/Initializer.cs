using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public DeckManager deckManager;
    // Start is called before the first frame update
    ArrayList deck;
    void Start()
    {
        generateTest();
        initializeDeck();
    }

    void initializeDeck()
    {
        //GameObject.Find("Canvas/Deck").GetComponent<DeckManager>().Init(deck);
        //GetComponentInChildren<DeckManager>().Init((ArrayList)deck.Clone());
        deckManager.Init((ArrayList)deck.Clone());
    }
    void generateTest()
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

}
