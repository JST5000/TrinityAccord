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

    public Button showAndHide;
    private bool menuHidden = false;
    private bool allowHide = false;

    private CardManager[] options;

    private CardData listener;

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
        for(int i = 0; i < givenOptions.Length; ++i) {
           // options[i].GetComponent<LayoutElement>().ignoreLayout = false;
            options[i].Init(givenOptions[i]);
        }
        for(int i = givenOptions.Length; i < options.Length; ++i)
        {
            options[i].SetEmpty();
            options[i].GetComponent<LayoutElement>().ignoreLayout = true;
        }
    }

    public void SendDecisionTo(CardData card)
    {
        listener = card;
    }

    public void AllowHide()
    {
        allowHide = true;
        CanvasGroupManip.Enable(showAndHide.GetComponent<CanvasGroup>());
    }

    public void ConfirmSelectedCard()
    {
        if(selectedCardMan != null || selectedCard != null)
        {
            if (selectedCardMan != null)
            {
                SendOutput(selectedCardMan.GetCardData());
            } else if(selectedCard != null)
            {
                SendOutput(selectedCard);
            }
            if (reloadEncounterOnDraft)
            {
                SceneManager.LoadScene("Encounter");
            }
            else
            {
                if (listener == null)
                {
                    Instantiate(ChooseEncounterMenu, GameObject.Find("Canvas").transform, false);
                }
                Destroy(gameObject);
            }
        }
    }

    private void SendOutput(CardData output)
    {
        if (listener != null)
        {
            CardData[] outputArr = { selectedCardMan.GetCardData() };
            listener.Action(outputArr);
        } else
        {
            PermanentState.AddCardToPlayerDeckList(output);
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

    public void ToggleVisibility()
    {
        CanvasGroup overallCG = GetComponent<CanvasGroup>();
        Text buttonText = showAndHide.GetComponentInChildren<Text>();
        if (menuHidden)
        {
            CanvasGroupManip.Enable(overallCG);
            buttonText.text = "Hide";
        } else
        {
            CanvasGroupManip.Disable(overallCG);
            buttonText.text = "Show";
        }
        menuHidden = !menuHidden;
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (!allowHide) {
            CanvasGroupManip.Disable(showAndHide.GetComponent<CanvasGroup>());
        }
        options = GetComponentsInChildren<CardManager>();
        CardData[] cards = CardDataUtil.ChooseNWithoutReplacement(CardPools.GetAllDraftableCards(), 3).ToArray();
        Init(cards);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
