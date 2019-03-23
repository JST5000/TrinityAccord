﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Choose3Manager : MonoBehaviour
{
    public static CardManager selectedCard = null;
    public Button confirm;
    public Transform ChooseEncounterMenu;
    public bool reloadEncounterOnDraft = false;
    private CardManager[] options;

    public void SetAndHighlightSelectedCard(CardManager newSelection)
    {
        if (selectedCard != null)
        {
            selectedCard.GetComponent<CardUIUpdater>().ResetHighlight();
        }
        selectedCard = newSelection;
        Debug.Log(newSelection);
        if (newSelection != null)
        {
            confirm.interactable = true;
            selectedCard.GetComponent<CardUIUpdater>().Highlight();
        }
    }

    public void Init(CardData[] givenOptions) 
    {
        selectedCard = null;
        for(int i = 0; i < options.Length; ++i) { 
            options[i].Init(givenOptions[i]);
        }
    }

    public void ConfirmSelectedCard()
    {
        if(selectedCard != null)
        {
            PermanentState.AddCardToPlayerDeckList(selectedCard.GetCardData());
            if (reloadEncounterOnDraft)
            {
                SceneManager.LoadScene("Encounter");
            }
            else
            {
                Instantiate(ChooseEncounterMenu, GameObject.Find("Canvas").transform, false);
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        options = GetComponentsInChildren<CardManager>();
        CardData[] cards = CardDataUtil.ChooseNWithoutReplacement(CardPools.GetAllCards(), 3).ToArray();
        Init(cards);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
