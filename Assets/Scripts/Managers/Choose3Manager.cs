using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choose3Manager : MonoBehaviour
{
    private static CardManager selectedCard = null;
    private CardManager[] options;

    //TODO
    public static void SetAndHighlightSelectedCard(CardManager newSelection)
    {
        if (selectedCard != null)
        {
            selectedCard.GetComponent<CardUIUpdater>().ResetHighlight();
        }
        selectedCard = newSelection;
        if (newSelection != null)
        {
            selectedCard.GetComponent<CardUIUpdater>().Highlight();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        options = GetComponentsInChildren<CardManager>();
        foreach(CardManager man in options)
        {
            man.Init(new Sword());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
