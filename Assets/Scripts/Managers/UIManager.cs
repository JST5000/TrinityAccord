using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum GameMode {PickTarget,PickAction,Animation }; 

public class UIManager : MonoBehaviour
{
    int currentMode = (int)GameMode.PickTarget;
    public const bool ENABLE_CLICK_BORDERS = false;
    private enum Status { USED, UNUSED };

    private void updateHitboxWithStatus(Status s, GameObject obj) 
    {
        if (ENABLE_CLICK_BORDERS)
        {
            DebugBorder border = obj.GetComponentInChildren<DebugBorder>();
            if (border != null)
            {
                Color output = Color.black;
                if (s == Status.USED)
                {
                    output = Color.green;
                }
                else
                {
                    output = Color.red;
                }
                border.SetRGBColorTemporarily(output);
           
            }
            
        }
    }

    public void clickEnemy(GameObject clicked)
    {
        Debug.Log("Clicked an Enemy named: " + clicked.name);
        updateHitboxWithStatus(Status.UNUSED, clicked);
    }

    public void clickCardInHand(GameObject clicked)
    {
        Debug.Log("Clicked a Card named: " + clicked.name);
        updateHitboxWithStatus(Status.UNUSED, clicked);
    }

    public void clickDeck(GameObject clicked)
    {
        Debug.Log("Clicked the Deck");
        updateHitboxWithStatus(Status.UNUSED, clicked);
    }

    public void clickDiscard(GameObject clicked)
    {
        Debug.Log("Clicked the Discard Pile");
        updateHitboxWithStatus(Status.UNUSED, clicked);
    }

    public void clickEndTurn(GameObject clicked)
    {
        Debug.Log("Clicked End Turn");
        updateHitboxWithStatus(Status.UNUSED, clicked);
    }

    public void clickBoard(GameObject clicked)
    { 
        Debug.Log("Clicked the Board");
        updateHitboxWithStatus(Status.UNUSED, clicked);
    }
}
