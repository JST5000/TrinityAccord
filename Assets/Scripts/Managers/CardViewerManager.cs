using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardViewerManager : MonoBehaviour
{
    public TextMeshProUGUI emptyMessage;
    public CanvasGroup leftButton;
    public CanvasGroup rightButton;

    CardManager[] options;
    //Inclusive
    private int lowerIndex;
    //Exclusive
    private int upperIndex;
    CardData[] cards;

    CanvasGroup overallCanvasGroup;

    public void Init(CardData[] givenOptions, bool startOnLeft = true)
    {
        cards = givenOptions;
        options = GetComponentsInChildren<CardManager>();
        int sizeOfOptions = Mathf.Min(givenOptions.Length, options.Length);
        if (startOnLeft)
        {
            lowerIndex = 0;
            upperIndex = sizeOfOptions;
        } else
        {
            upperIndex = givenOptions.Length;
            lowerIndex = givenOptions.Length - sizeOfOptions;
        }

        LoadVisibleCards();

        UpdateLeftRightButtons();

        CanvasGroupManip.SetVisibility(givenOptions.Length == 0, emptyMessage.GetComponent<CanvasGroup>());

        //Should be last, displays the whole object
        CanvasGroup overallCG = GetComponent<CanvasGroup>();
        CanvasGroupManip.Disable(overallCG);
        CanvasGroupManip.Enable(overallCG);
    }

    //Depends on cards, lowerIndex and upperIndex as implicit inputs
    private void LoadVisibleCards()
    {
        for (int i = 0; i < cards.Length 
            && i < options.Length 
            && i < (upperIndex - lowerIndex);
            ++i)
        {
            // options[i].GetComponent<LayoutElement>().ignoreLayout = false;
            options[i].Init(cards[i + lowerIndex]);
        }

        for (int i = cards.Length; i < options.Length; ++i)
        {
            options[i].SetEmpty();
            options[i].GetComponent<LayoutElement>().ignoreLayout = true;
        }

    }

    public void Exit()
    {
        CanvasGroupManip.Disable(overallCanvasGroup);
    }

    public void ShiftLeft()
    {
        if(lowerIndex != 0)
        {
            lowerIndex--;
            upperIndex--;
            LoadVisibleCards();
            UpdateLeftRightButtons();
        }
    }

    public void ShiftRight()
    {
        if(upperIndex != cards.Length)
        {
            lowerIndex++;
            upperIndex++;
            LoadVisibleCards();
            UpdateLeftRightButtons();
        }
    }

    private void UpdateLeftRightButtons()
    {
        CanvasGroupManip.SetVisibility(lowerIndex != 0, leftButton);
        CanvasGroupManip.SetVisibility(upperIndex != cards.Length, rightButton);
    }

    // Start is called before the first frame update
    void Start()
    {
        CardData[] defaultCards = { new Sword(), new Lightning(), new VileSword(), new Peer() };
        Init(defaultCards);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
