using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Choose3Manager : MonoBehaviour
{
    public static CardManager selectedCardMan = null;
    public static CardData selectedCard = null;
    public Button confirm;
    public Transform ChooseEncounterMenu;
    public bool reloadEncounterOnDraft = false;
    private CardManager[] options;

    public Text errorMessage;
    public void SetAndHighlightSelectedCard(CardManager newSelection)
    {
        if (selectedCardMan != null)
        {
            selectedCardMan.GetComponent<CardUIUpdater>().ResetHighlight();
        }
        selectedCardMan = newSelection;
        Debug.Log(newSelection);
        if (newSelection != null)
        {
            confirm.interactable = true;
            selectedCardMan.GetComponent<CardUIUpdater>().Highlight();
        }
    }

    public void Init(CardData[] givenOptions) 
    {
        selectedCardMan = null;
        for(int i = 0; i < options.Length; ++i) { 
            options[i].Init(givenOptions[i]);
        }
    }

    public void ConfirmSelectedCard()
    {
        if(selectedCardMan != null || selectedCard != null)
        {
            if (selectedCardMan != null)
            {
                PermanentState.AddCardToPlayerDeckList(selectedCardMan.GetCardData());
            } else if(selectedCard != null)
            {
                PermanentState.AddCardToPlayerDeckList(selectedCard);
            }
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

    public void EnterCustomFight(TextMeshProUGUI inputField)
    {
        string input = inputField.text.Substring(0, inputField.text.Length - 1);
        try
        {
            selectedCard = CardDataUtil.InterpretText(input)[0];
            if(selectedCardMan != null)
            {
                selectedCardMan.GetComponent<CardUIUpdater>().ResetHighlight();
                selectedCardMan = null;
            }
            confirm.interactable = true;
            errorMessage.text = "";
        }
        catch (KeyNotFoundException)
        {
            confirm.interactable = false;
            errorMessage.text = "Error: Given text is not a valid card";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        options = GetComponentsInChildren<CardManager>();
        CardData[] cards = CardDataUtil.ChooseNWithoutReplacement(CardPools.GetAllDraftableCards(), 3).ToArray();
        Init(cards);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
