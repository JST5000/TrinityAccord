using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class holds functions that will slowly check for Quest conditions, then will update the encounter deck when they trigger.
/// </summary>
public class QuestListener : MonoBehaviour
{
    //This sets up the initial condition for all quests in for a new encounter
    public static void InitializeQuests()
    {

    }

    public static void TriggerQuest(string cardName)
    {
        List<CardData> cardsWithName = DeckManager.Get().GetCardsWithName(cardName);
        foreach(CardData card in cardsWithName)
        {
            card.Transformed = true;
        }
    }

    /// <summary>
    /// Should be called when an enemy is disarmed and when an enemy leaves the fight (Death or run)
    /// </summary>
    public static void UpdateApprentice()
    {
        foreach(EnemyManager man in EncounterManager.Get().allEnemyManagers) {
            if(!man.IsEmpty() && !man.GetEnemyData().HasBeenDisarmed)
            {
                return;
            }
        }
        TriggerQuest(new Apprentice().GetName());
    }

    /// <summary>
    /// Should only be called by an enemy on death
    /// </summary>
    public static void UpdateAssassin()
    {
        TriggerQuest(new Assassin().GetName());
    }

    public static void UpdateAssault()
    {
        Assault assault = new Assault();
        if (StackManager.Get().AttacksPlayedThisEncounter >= assault.AttacksToTrigger)
        {
            TriggerQuest(assault.GetName());
        }
    }

    public static void UpdateArcana()
    {
        Arcana arcana = new Arcana();
        if (StackManager.Get().SpellsPlayedThisEncounter >= arcana.SpellsToCompleteQuest)
        {
            TriggerQuest(arcana.GetName());
        }
    }


}
