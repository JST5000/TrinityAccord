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

    public void Init(CardData[] givenOptions, bool startOnLeft = true)
    {
        cards = givenOptions;
        Debug.Log(cards.Length + " DISCARD LENGTH");
        options = GetComponentsInChildren<CardManager>();
        int sizeOfOptions = Mathf.Min(givenOptions.Length, options.Length);
        Debug.Log("Size of options; " + sizeOfOptions);
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


        CanvasGroupManip.SetVisibility(givenOptions.Length == 0, emptyMessage.GetComponent<CanvasGroup>());

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        //Should be last, displays the whole object
        CanvasGroup overallCG = GetComponent<CanvasGroup>();
        CanvasGroupManip.Refresh(leftButton);
        CanvasGroupManip.Refresh(rightButton);
        CanvasGroupManip.Refresh(overallCG);
        UpdateLeftRightButtons();
    }

    //Depends on cards, lowerIndex and upperIndex as implicit inputs
    private void LoadVisibleCards()
    {
        for (int i = 0; i < cards.Length 
            && i < options.Length 
            && i < (upperIndex - lowerIndex);
            ++i)
        {
            options[i].GetComponent<LayoutElement>().ignoreLayout = false;
            options[i].Init(cards[i + lowerIndex]);
        }

        for (int i = cards.Length; i < options.Length; ++i)
        {
            options[i].SetEmpty();
            options[i].GetComponent<LayoutElement>().ignoreLayout = true;
        }
        UpdateLeftRightButtons();
    }

    public void Exit()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 2, Screen.height * 2, 0));
        CanvasGroupManip.Disable(GetComponent<CanvasGroup>());
    }

    public void ShiftLeft()
    {
        if(lowerIndex != 0)
        {
            lowerIndex--;
            upperIndex--;
            LoadVisibleCards();
        }
    }

    public void ShiftRight()
    {
        if(upperIndex != cards.Length)
        {
            lowerIndex++;
            upperIndex++;
            LoadVisibleCards();
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
