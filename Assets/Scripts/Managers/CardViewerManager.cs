using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardViewerManager : MonoBehaviour
{
    public TextMeshProUGUI emptyMessage;

    CardManager[] options;
    CanvasGroup overallCanvasGroup;


    public void Init(CardData[] givenOptions)
    {
        overallCanvasGroup = GetComponent<CanvasGroup>();
        GetComponent<CanvasGroup>();

        options = GetComponentsInChildren<CardManager>();

        for (int i = 0; i < givenOptions.Length; ++i)
        {
            // options[i].GetComponent<LayoutElement>().ignoreLayout = false;
            options[i].Init(givenOptions[i]);
        }
        for (int i = givenOptions.Length; i < options.Length; ++i)
        {
            options[i].SetEmpty();
            options[i].GetComponent<LayoutElement>().ignoreLayout = true;
        }
        CanvasGroupManip.Enable(overallCanvasGroup);
        if(givenOptions.Length == 0)
        {
            CanvasGroupManip.Disable(emptyMessage.GetComponent<CanvasGroup>());
        } else
        {
            CanvasGroupManip.Enable(emptyMessage.GetComponent<CanvasGroup>());
        }
    }

    public void Exit()
    {
        CanvasGroupManip.Disable(overallCanvasGroup);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(options.Length == 0)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
