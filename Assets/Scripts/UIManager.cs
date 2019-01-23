using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public const bool ENABLE_CLICK_BORDERS = true;

    public void clickEnemy(GameObject clicked)
    {
        Debug.Log("Clicked an Enemy named: " + clicked.name);
    }

    public void clickCardInHand(GameObject clicked)
    {
        Debug.Log("Clicked a Card named: " + clicked.name);
    }

    public void clickDeck(GameObject clicked)
    {
        Debug.Log("Clicked the Deck");
    }

    public void clickDiscard(GameObject clicked)
    {
        Debug.Log("Clicked the Discard Pile");
    }

    public void clickEndTurn(GameObject clicked)
    {
        Debug.Log("Clicked End Turn");
    }

    public void clickBoard(GameObject clicked)
    {
        Debug.Log("Clicked the Board");
    }
}
