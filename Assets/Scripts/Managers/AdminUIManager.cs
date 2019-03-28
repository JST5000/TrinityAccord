﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminUIManager : MonoBehaviour
{
    public Transform ChooseEncounterMenu;
    public Transform DraftCardMenu;

    public void ChooseEncounter()
    {
        Instantiate(ChooseEncounterMenu, GameObject.Find("Canvas").transform, false);
    }

    public void DraftCard()
    {
        Transform instance = Instantiate(DraftCardMenu, GameObject.Find("Canvas").transform, false);
        //Causes the encounter to reset after drafting the card
        instance.GetComponent<Choose3Manager>().reloadEncounterOnDraft = true;
    }

}